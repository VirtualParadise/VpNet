using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using VpNet.NativeApi;

namespace VpNet
{
    public partial class VirtualParadiseClient
    {
        // These collections are here to prevent the delegate objects from being garbage collected. Do not remove.
        private readonly Dictionary<Events, EventDelegate> _nativeEvents = new();
        private readonly Dictionary<Callbacks, CallbackDelegate> _nativeCallbacks = new();

        private void SetNativeEvent(Events eventType, EventDelegate eventFunction)
        {
            _nativeEvents[eventType] = eventFunction;
            Functions.vp_event_set(NativeInstanceHandle, (int) eventType, eventFunction);
        }

        private void SetNativeCallback(Callbacks callbackType, CallbackDelegate callbackFunction)
        {
            _nativeCallbacks[callbackType] = callbackFunction;
            Functions.vp_callback_set(NativeInstanceHandle, (int) callbackType, callbackFunction);
        }

        private void OnLoginCallbackNative(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_loginCompletionSource, rc, null);
            }
        }

        private void OnEnterCallbackNativeEvent(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_enterCompletionSource, rc, null);
                WorldEntered?.Invoke(this, new WorldEnterEventArgs(World));
            }
        }

        private void OnConnectUniverseCallbackNative(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_connectCompletionSource, rc, null);
            }
        }

        private void OnObjectCreateCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(reference, rc, Functions.vp_int(sender, IntegerAttribute.ObjectId));
            }
        }

        private void OnObjectChangeCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(reference, rc, null);
            }
        }

        private void OnObjectDeleteCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(reference, rc, null);
            }
        }

        private void OnObjectGetCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                GetVpObject(sender, out VpObject vpObject);

                SetCompletionResult(reference, rc, vpObject);
            }
        }

        private void OnObjectLoadCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(reference, rc, Functions.vp_int(sender, IntegerAttribute.ObjectId));
            }
        }

        private void OnUserAttributesNative(IntPtr sender)
        {
            User user;
            int userId;

            lock (this)
            {
                userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                string name = Functions.vp_string(sender, StringAttribute.UserName);
                string email = Functions.vp_string(sender, StringAttribute.UserEmail);
                int lastLoginTimestamp = Functions.vp_int(sender, IntegerAttribute.UserLastLogin);
                int registrationDateTimestamp = Functions.vp_int(sender, IntegerAttribute.UserRegistrationTime);
                int onlineTimeSeconds = Functions.vp_int(sender, IntegerAttribute.UserOnlineTime);

                DateTimeOffset lastLogin = DateTimeOffset.FromUnixTimeSeconds(lastLoginTimestamp);
                DateTimeOffset registrationDate = DateTimeOffset.FromUnixTimeSeconds(registrationDateTimestamp);
                TimeSpan onlineTime = TimeSpan.FromSeconds(onlineTimeSeconds);

                user = new User
                {
                    Id = userId,
                    Name = name,
                    Email = email,
                    LastLogin = lastLogin,
                    OnlineTime = onlineTime,
                    RegistrationDate = registrationDate
                };
            }

            if (_userCompletionSources.TryGetValue(userId, out TaskCompletionSource<object> taskCompletionSource))
            {
                _userCompletionSources.Remove(userId);
                SetCompletionResult(taskCompletionSource, 0, user);
            }
        }

        private void OnTeleportNative(IntPtr sender)
        {
            if (Teleported == null) return;

            TeleportEventArgs args;

            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                Avatar avatar = GetAvatar(session);

                Vector3 position = new Vector3
                {
                    X = Functions.vp_double(sender, FloatAttribute.TeleportX),
                    Y = Functions.vp_double(sender, FloatAttribute.TeleportY),
                    Z = Functions.vp_double(sender, FloatAttribute.TeleportZ)
                };
                Vector3 rotation = new Vector3
                {
                    X = Functions.vp_double(sender, FloatAttribute.TeleportPitch),
                    Y = Functions.vp_double(sender, FloatAttribute.TeleportYaw),
                    Z = 0 // roll not implemented
                };

                string worldName = Functions.vp_string(sender, StringAttribute.TeleportWorld);
                var location = new Location(worldName, position, rotation);

                args = new TeleportEventArgs(avatar, location);
            }

            Debug.Assert(!(Teleported is null), $"{nameof(Teleported)} != null");
            Teleported.Invoke(this, args);
        }

        private void OnGetFriendsCallbackNative(IntPtr sender, int rc, int reference)
        {
            if (FriendAdded == null)
                return;

            int userId;
            string name;
            bool isOnline;

            lock (this)
            {
                userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                name = Functions.vp_string(sender, StringAttribute.FriendName);
                isOnline = Functions.vp_int(sender, IntegerAttribute.FriendOnline) == 1;
            }

            var friend = new Friend(userId, name, isOnline);
            var args = new FriendsGetCallbackEventArgs(friend);

            Debug.Assert(!(FriendReceived is null), $"{nameof(FriendReceived)} != null");
            FriendReceived.Invoke(this, args);
        }

        private void OnFriendDeleteCallbackNative(IntPtr sender, int rc, int reference)
        {
            // todo: implement this.
        }

        private void OnFriendAddCallbackNative(IntPtr sender, int rc, int reference)
        {
            // todo: implement this.
        }

        private void OnChatNative(IntPtr sender)
        {
            if (ChatMessageReceived is null)
                return;

            Avatar avatar;
            ChatMessage message;

            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                avatar = GetAvatar(session);
                
                var type = (ChatMessageTypes) Functions.vp_int(sender, IntegerAttribute.ChatType);
                var effects = (TextEffectTypes) Functions.vp_int(sender, IntegerAttribute.ChatEffects);
                string text = Functions.vp_string(sender, StringAttribute.ChatMessage);
                string name = Functions.vp_string(sender, StringAttribute.AvatarName);
                Color color = new Color(0, 0, 0);

                if (type == ChatMessageTypes.Console)
                {
                    byte r = (byte) Functions.vp_int(sender, IntegerAttribute.ChatColorRed);
                    byte g = (byte) Functions.vp_int(sender, IntegerAttribute.ChatColorGreen);
                    byte b = (byte) Functions.vp_int(sender, IntegerAttribute.ChatColorBlue);

                    color = new Color(r, g, b);
                }

                message = new ChatMessage(name, text, type, color, effects);
            }

            var args = new ChatMessageEventArgs(avatar, message);
            ChatMessageReceived.Invoke(this, args);
        }

        private async void OnAvatarAddNative(IntPtr sender)
        {
            Avatar avatar;
            int userId;

            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                int type = Functions.vp_int(sender, IntegerAttribute.AvatarType);
                string name = Functions.vp_string(sender, StringAttribute.AvatarName);

                double x = Functions.vp_double(sender, FloatAttribute.AvatarX);
                double y = Functions.vp_double(sender, FloatAttribute.AvatarY);
                double z = Functions.vp_double(sender, FloatAttribute.AvatarZ);

                double pitch = Functions.vp_double(sender, FloatAttribute.AvatarPitch);
                double yaw = Functions.vp_double(sender, FloatAttribute.AvatarYaw);

                string applicationName = Functions.vp_string(sender, StringAttribute.ApplicationName);
                string applicationVersion = Functions.vp_string(sender, StringAttribute.ApplicationVersion);

                var position = new Vector3(x, y, z);
                var rotation = new Vector3(pitch, yaw, 0);

                var location = new Location(World, position, rotation);

                avatar = new Avatar
                {
                    Session = session,
                    Name = name,
                    AvatarType = type,
                    Location = location,
                    LastChanged = DateTimeOffset.Now,
                    ApplicationName = applicationName,
                    ApplicationVersion = applicationVersion
                };

                if (_avatars.ContainsKey(session)) _avatars[session] = avatar;
                else _avatars.Add(session, avatar);
            }

            avatar.User = await GetCachedUserAsync(userId); // cannot await in lock

            if (AvatarEntered is null) return;

            var args = new AvatarEnterEventArgs(avatar);
            AvatarEntered?.Invoke(this, args);
        }

        private void OnAvatarChangeNative(IntPtr sender)
        {
            Avatar avatar;
            Avatar oldAvatar = null;
            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);

                if (_avatars.TryGetValue(session, out avatar))
                    oldAvatar = (Avatar) avatar.Clone();
                else
                    avatar = new Avatar();

                double x = Functions.vp_double(sender, FloatAttribute.AvatarX);
                double y = Functions.vp_double(sender, FloatAttribute.AvatarY);
                double z = Functions.vp_double(sender, FloatAttribute.AvatarZ);

                double pitch = Functions.vp_double(sender, FloatAttribute.AvatarPitch);
                double yaw = Functions.vp_double(sender, FloatAttribute.AvatarYaw);

                avatar.Name = Functions.vp_string(sender, StringAttribute.AvatarName);
                avatar.AvatarType = Functions.vp_int(sender, IntegerAttribute.AvatarType);

                var location = avatar.Location;
                location.Position = new Vector3(x, y, z);
                location.Rotation = new Vector3(pitch, yaw, 0);
                avatar.Location = location;
                avatar.LastChanged = DateTimeOffset.Now;
            }

            AvatarChanged?.Invoke(this, new AvatarChangeEventArgs(avatar, oldAvatar));
        }

        private void OnAvatarDeleteNative(IntPtr sender)
        {
            Avatar avatar;

            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                if (!_avatars.TryGetValue(session, out avatar))
                    return;

                _avatars.Remove(session);
            }

            AvatarLeft?.Invoke(this, new AvatarLeaveEventArgs(avatar));
        }

        private void OnAvatarClickNative(IntPtr sender)
        {
            if (AvatarClicked is null)
                return;

            int avatarSession;
            int clickedSession;
            Vector3 hitPoint;

            lock (this)
            {
                avatarSession = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                clickedSession = Functions.vp_int(sender, IntegerAttribute.ClickedSession);

                if (clickedSession == 0)
                    clickedSession = avatarSession;

                double hitX = Functions.vp_double(sender, FloatAttribute.ClickHitX);
                double hitY = Functions.vp_double(sender, FloatAttribute.ClickHitY);
                double hitZ = Functions.vp_double(sender, FloatAttribute.ClickHitZ);
                hitPoint = new Vector3(hitX, hitY, hitZ);
            }

            var avatar = GetAvatar(avatarSession);
            var clickedAvatar = GetAvatar(clickedSession);
            var args = new AvatarClickEventArgs(avatar, clickedAvatar, hitPoint);

            Debug.Assert(!(AvatarClicked is null), $"{nameof(AvatarClicked)} != null");
            AvatarClicked.Invoke(this, args);
        }

        private void OnObjectClickNative(IntPtr sender)
        {
            if (ObjectClicked is null)
                return;

            int session;
            int objectId;
            Vector3 hitPoint;

            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                objectId = Functions.vp_int(sender, IntegerAttribute.ObjectId);
                double hitX = Functions.vp_double(sender, FloatAttribute.ClickHitX);
                double hitY = Functions.vp_double(sender, FloatAttribute.ClickHitY);
                double hitZ = Functions.vp_double(sender, FloatAttribute.ClickHitZ);
                hitPoint = new Vector3(hitX, hitY, hitZ);
            }

            var avatar = GetAvatar(session);
            var vpObject = new VpObject { Id = objectId };
            var args = new ObjectClickArgs(avatar, vpObject, hitPoint);

            Debug.Assert(!(ObjectClicked is null), $"{nameof(ObjectClicked)} != null");
            ObjectClicked.Invoke(this, args);
        }

        private void OnObjectBumpNative(IntPtr sender)
        {
            if (ObjectBumped == null)
                return;

            int session;
            int objectId;

            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                objectId = Functions.vp_int(sender, IntegerAttribute.ObjectId);
            }

            var avatar = GetAvatar(session);
            var vpObject = new VpObject { Id = objectId };
            var args = new ObjectBumpArgs(avatar, vpObject, BumpType.BumpBegin);

            Debug.Assert(!(ObjectBumped is null), $"{nameof(ObjectBumped)} != null");
            ObjectBumped.Invoke(this, args);
        }

        private void OnObjectBumpEndNative(IntPtr sender)
        {
            if (ObjectBumped == null)
                return;

            int session;
            int objectId;

            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                objectId = Functions.vp_int(sender, IntegerAttribute.ObjectId);
            }

            var avatar = GetAvatar(session);
            var vpObject = new VpObject { Id = objectId };
            var args = new ObjectBumpArgs(avatar, vpObject, BumpType.BumpEnd);

            Debug.Assert(!(ObjectBumped is null), $"{nameof(ObjectBumped)} != null");
            ObjectBumped.Invoke(this, args);
        }

        private void OnObjectDeleteNative(IntPtr sender)
        {
            if (ObjectDeleted == null)
                return;

            int session;
            int objectId;

            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                objectId = Functions.vp_int(sender, IntegerAttribute.ObjectId);
            }

            var avatar = GetAvatar(session);
            var vpObject = new VpObject { Id = objectId };
            var args = new ObjectDeleteArgs(avatar, vpObject);

            Debug.Assert(!(ObjectDeleted is null), $"{nameof(ObjectDeleted)} != null");
            ObjectDeleted.Invoke(this, args);
        }

        private void OnObjectCreateNative(IntPtr sender)
        {
            int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
            GetVpObject(sender, out VpObject vpObject);

            if (session == 0)
            {
                _currentCellObjects.Add(vpObject);
            }
            else
            {
                var avatar = GetAvatar(session);
                ObjectCreated?.Invoke(this, new ObjectCreateArgs(avatar, vpObject));
            }
        }

        private void OnObjectChangeNative(IntPtr sender)
        {
            if (ObjectChanged == null) return;
            VpObject vpObject;
            int sessionId;
            lock (this)
            {
                GetVpObject(sender, out vpObject);
                sessionId = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
            }

            ObjectChanged(this, new ObjectChangeArgs(GetAvatar(sessionId), vpObject));
        }

        private void OnQueryCellEndNative(IntPtr sender)
        {
            int x = Functions.vp_int(sender, IntegerAttribute.CellX);
            int z = Functions.vp_int(sender, IntegerAttribute.CellZ);

            var index = _queryCellCompletionSources.FindIndex(cell => cell.Cell == new Cell(x, z));
            var item = _queryCellCompletionSources[index];
            _queryCellCompletionSources.RemoveAt(index);
            item.CompletionSource.SetResult(new QueryCellResult
            {
                Status = (CellStatus)Functions.vp_int(sender, IntegerAttribute.CellStatus),
                Revision = Functions.vp_int(sender, IntegerAttribute.CellRevision),
                Objects = _currentCellObjects
            });
            _currentCellObjects = new List<VpObject>();
        }

        private void OnWorldListNative(IntPtr sender)
        {
            if (WorldListEntryReceived == null)
                return;

            World data;
            lock (this)
            {
                string worldName = Functions.vp_string(NativeInstanceHandle, StringAttribute.WorldName);
                data = new World()
                {
                    Name = worldName,
                    State = (WorldState) Functions.vp_int(NativeInstanceHandle, IntegerAttribute.WorldState),
                    UserCount = Functions.vp_int(NativeInstanceHandle, IntegerAttribute.WorldUsers)
                };
            }

            if (_worlds.ContainsKey(data.Name))
                _worlds.Remove(data.Name);
            _worlds.Add(data.Name, data);
            WorldListEntryReceived(this, new WorldListEventArgs(data));
        }

        private void OnWorldSettingNative(IntPtr instance)
        {
            if (!_worlds.ContainsKey(Configuration.World.Name))
            {
                _worlds.Add(Configuration.World.Name, Configuration.World);
            }

            var world = _worlds[Configuration.World.Name];
            var key = Functions.vp_string(instance, StringAttribute.WorldSettingKey);
            var value = Functions.vp_string(instance, StringAttribute.WorldSettingValue);
            world.RawAttributes[key] = value;
        }

        private void OnWorldSettingsChangedNative(IntPtr instance)
        {
            // Initialize World Object Cache if a local object path has been specified and a objectpath is speficied in the world attributes.
            // TODO: some world, such as Test do not specify a objectpath, maybe there's a default search path we dont know of.
            var world = _worlds[Configuration.World.Name];

            WorldSettingsChanged?.Invoke(this, new WorldSettingsChangedEventArgs(_worlds[Configuration.World.Name]));
        }

        private void OnUniverseDisconnectNative(IntPtr sender)
        {
            CurrentUser = null;
            
            if (UniverseDisconnected == null) return;
            UniverseDisconnected(this, new UniverseDisconnectEventArgs(Universe));
        }

        private void OnWorldDisconnectNative(IntPtr sender)
        {
            if (WorldDisconnected == null) return;
            WorldDisconnected(this, new WorldDisconnectEventArgs(World));
        }

        private void OnJoinNative(IntPtr sender)
        {
            if (JoinRequestReceived == null) return;

            lock (this)
            {
                int requestId = Functions.vp_int(sender, IntegerAttribute.JoinId);
                int userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                string name = Functions.vp_string(sender, StringAttribute.JoinName);

                var request = new JoinRequest(this, requestId, userId, name);
                var args = new JoinEventArgs(request);
                JoinRequestReceived?.Invoke(this, args);
            }
        }

        private void OnTerrainNodeNative(IntPtr sender)
        {
            var node = new TerrainNode(sender);
            _currentTerrainNodes.Add(node);
		}

		private void OnTerrainQueryCallbackNative(IntPtr sender, int rc, int reference)
		{
			SetCompletionResult(reference, rc, _currentTerrainNodes.ToArray());
			_currentTerrainNodes.Clear();
		}

		private void OnTerrainNodeSetCallbackNative(IntPtr sender, int rc, int reference)
		{
			SetCompletionResult(reference, rc, null);
		}
	}
}
