using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using VpNet.NativeApi;

namespace VpNet
{
    public partial class VirtualParadiseClient
    {
        internal event EventDelegate OnChatNativeEvent;
        internal event EventDelegate OnAvatarAddNativeEvent;
        internal event EventDelegate OnAvatarDeleteNativeEvent;
        internal event EventDelegate OnAvatarChangeNativeEvent;
        internal event EventDelegate OnAvatarClickNativeEvent;
        internal event EventDelegate OnWorldListNativeEvent;
        internal event EventDelegate OnObjectChangeNativeEvent;
        internal event EventDelegate OnObjectCreateNativeEvent;
        internal event EventDelegate OnObjectDeleteNativeEvent;
        internal event EventDelegate OnObjectClickNativeEvent;
        internal event EventDelegate OnObjectBumpNativeEvent;
        internal event EventDelegate OnObjectBumpEndNativeEvent;
        internal event EventDelegate OnQueryCellEndNativeEvent;
        internal event EventDelegate OnUniverseDisconnectNativeEvent;
        internal event EventDelegate OnWorldDisconnectNativeEvent;
        internal event EventDelegate OnTeleportNativeEvent;
        internal event EventDelegate OnUserAttributesNativeEvent;
        internal event EventDelegate OnJoinNativeEvent;

        internal event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        internal event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        internal event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        internal event CallbackDelegate OnObjectGetCallbackNativeEvent;
        internal event CallbackDelegate OnObjectLoadCallbackNativeEvent;
        internal event CallbackDelegate OnFriendAddCallbackNativeEvent;
        internal event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        internal event CallbackDelegate OnGetFriendsCallbackNativeEvent;
        internal event CallbackDelegate OnJoinCallbackNativeEvent;
        internal event CallbackDelegate OnWorldPermissionUserSetCallbackNativeEvent;
        internal event CallbackDelegate OnWorldPermissionSessionSetCallbackNativeEvent;
        internal event CallbackDelegate OnWorldSettingsSetCallbackNativeEvent;

        internal void OnObjectCreateCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnObjectCreateCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnObjectChangeCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnObjectChangeCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnObjectDeleteCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnObjectDeleteCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnObjectGetCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnObjectGetCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnFriendAddCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnFriendAddCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnFriendDeleteCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnFriendDeleteCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnGetFriendsCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnFriendDeleteCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnObjectLoadCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnObjectLoadCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnLoginCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_loginCompletionSource, rc, null);
            }
        }

        internal void OnEnterCallbackNativeEvent1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_enterCompletionSource, rc, null);
                WorldEntered?.Invoke(this, new WorldEnterEventArgs(World));
            }
        }

        internal void OnJoinCallbackNativeEvent1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnJoinCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnConnectUniverseCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_connectCompletionSource, rc, null);
            }
        }

        internal void OnWorldPermissionUserSetCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnWorldPermissionUserSetCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnWorldPermissionSessionSetCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnWorldPermissionSessionSetCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnWorldSettingsSetCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                OnWorldSettingsSetCallbackNativeEvent(instance, rc, reference);
            }
        }

        internal void OnChatNative1(IntPtr instance)
        {
            lock (this)
            {
                OnChatNativeEvent(instance);
            }
        }

        internal void OnAvatarAddNative1(IntPtr instance)
        {
            lock (this)
            {
                OnAvatarAddNativeEvent(instance);
            }
        }

        internal void OnAvatarChangeNative1(IntPtr instance)
        {
            lock (this)
            {
                OnAvatarChangeNativeEvent(instance);
            }
        }

        internal void OnAvatarDeleteNative1(IntPtr instance)
        {
            lock (this)
            {
                OnAvatarDeleteNativeEvent(instance);
            }
        }

        internal void OnAvatarClickNative1(IntPtr instance)
        {
            lock (this)
            {
                OnAvatarClickNativeEvent(instance);
            }
        }

        internal void OnWorldListNative1(IntPtr instance)
        {
            lock (this)
            {
                OnWorldListNativeEvent(instance);
            }
        }

        internal void OnWorldDisconnectNative1(IntPtr instance)
        {
            lock (this)
            {
                OnWorldDisconnectNativeEvent(instance);
            }
        }

        internal void OnWorldSettingsChangedNative1(IntPtr instance)
        {
            lock (this)
            {
                OnWorldSettingsChangedNativeEvent(instance);
            }
        }

        internal void OnWorldSettingNative1(IntPtr instance)
        {
            lock (this)
            {
                OnWorldSettingNativeEvent(instance);
            }
        }

        internal void OnObjectChangeNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectChangeNativeEvent(instance);
            }
        }

        internal void OnObjectCreateNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectCreateNativeEvent(instance);
            }
        }

        internal void OnObjectClickNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectClickNativeEvent(instance);
            }
        }

        internal void OnObjectBumpNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectBumpNativeEvent(instance);
            }
        }

        internal void OnObjectBumpEndNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectBumpEndNativeEvent(instance);
            }
        }

        internal void OnObjectDeleteNative1(IntPtr instance)
        {
            lock (this)
            {
                OnObjectDeleteNativeEvent(instance);
            }
        }

        internal void OnQueryCellEndNative1(IntPtr instance)
        {
            lock (this)
            {
                OnQueryCellEndNativeEvent(instance);
            }
        }

        internal void OnUniverseDisconnectNative1(IntPtr instance)
        {
            lock (this)
            {
                OnUniverseDisconnectNativeEvent(instance);
            }
        }

        internal void OnTeleportNative1(IntPtr instance)
        {
            lock (this)
            {
                OnTeleportNativeEvent(instance);
            }
        }

        internal void OnUserAttributesNative1(IntPtr instance)
        {
            lock (this)
            {
                OnUserAttributesNativeEvent(instance);
            }
        }

        internal void OnJoinNative1(IntPtr instance)
        {
            lock (this)
            {
                OnJoinNativeEvent(instance);
            }
        }

        private readonly Dictionary<Events, EventDelegate> _nativeEvents = new Dictionary<Events, EventDelegate>();

        private readonly Dictionary<Callbacks, CallbackDelegate> _nativeCallbacks = new Dictionary<Callbacks, CallbackDelegate>();

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
            UserAttributes attributes;

            lock (this)
            {
                int userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                string name = Functions.vp_string(sender, StringAttribute.UserName);
                string email = Functions.vp_string(sender, StringAttribute.UserEmail);
                int lastLoginTimestamp = Functions.vp_int(sender, IntegerAttribute.UserLastLogin);
                int registrationDateTimestamp = Functions.vp_int(sender, IntegerAttribute.UserRegistrationTime);
                int onlineTimeSeconds = Functions.vp_int(sender, IntegerAttribute.UserOnlineTime);

                DateTimeOffset lastLogin = DateTimeOffset.FromUnixTimeSeconds(lastLoginTimestamp);
                DateTimeOffset registrationDate = DateTimeOffset.FromUnixTimeSeconds(registrationDateTimestamp);
                TimeSpan onlineTime = TimeSpan.FromSeconds(onlineTimeSeconds);

                attributes = new UserAttributes(userId, name, email, lastLogin.UtcDateTime, onlineTime, registrationDate.UtcDateTime);
            }

            var args = new UserAttributesEventArgs(attributes);
            UserAttributesReceived?.Invoke(this, args);
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

                var location = new Location(World, Vector3.Zero, Rotation.Zero);

                if (!_avatars.TryGetValue(session, out avatar))
                    _avatars.Add(session,
                        avatar = new Avatar(0, session, name, 0, location, DateTimeOffset.Now, string.Empty, string.Empty));

                message = new ChatMessage(name, text, type, color, effects);
            }

            var args = new ChatMessageEventArgs(avatar, message);
            ChatMessageReceived.Invoke(this, args);
        }

        private void OnAvatarAddNative(IntPtr sender)
        {
            Avatar avatar;

            lock (this)
            {
                int session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
                int userId = Functions.vp_int(sender, IntegerAttribute.UserId);
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

                avatar = new Avatar(userId, 0, name, type, location, DateTimeOffset.Now,
                    applicationName, applicationVersion);

                if (_avatars.ContainsKey(session))
                    _avatars[session] = avatar;
                else
                    _avatars.Add(session, avatar);
            }

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
                location.Rotation = new Rotation(pitch, yaw);
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
            if (ObjectCreated is null && QueryCellResult is null)
                return;

            int session;

            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
            }

            var avatar = GetAvatar(session);

            GetVpObject(sender, out VpObject vpObject);

            if (session == 0)
                QueryCellResult?.Invoke(this, new QueryCellResultArgs(vpObject));
            else
                ObjectCreated?.Invoke(this, new ObjectCreateArgs(avatar, vpObject));
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
            if (QueryCellEnd == null) return;
            int x;
            int z;
            lock (this)
            {
                x = Functions.vp_int(sender, IntegerAttribute.CellX);
                z = Functions.vp_int(sender, IntegerAttribute.CellZ);
            }

            QueryCellEnd(this, new QueryCellEndArgs(new Cell(x, z)));
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

        private void OnWorldSettingNativeEvent(IntPtr instance)
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

        private void OnWorldSettingsChangedNativeEvent(IntPtr instance)
        {
            // Initialize World Object Cache if a local object path has been specified and a objectpath is speficied in the world attributes.
            // TODO: some world, such as Test do not specify a objectpath, maybe there's a default search path we dont know of.
            var world = _worlds[Configuration.World.Name];

            WorldSettingsChanged?.Invoke(this, new WorldSettingsChangedEventArgs(_worlds[Configuration.World.Name]));
        }

        private void OnUniverseDisconnectNative(IntPtr sender)
        {
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
    }
}
