using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using VpNet.ManagedApi.System;
using VpNet.NativeApi;

namespace VpNet
{
    /// <summary>
    ///     Provides a managed API which offers full encapsulation of the native SDK.
    /// </summary>
    public partial class VirtualParadiseClient
    {
        private const string DefaultUniverseHost = "universe.virtualparadise.org";
        private const int DefaultUniversePort = 57000;

        private readonly Dictionary<int, TaskCompletionSource<object>> _objectCompletionSources;
        private readonly Dictionary<int, TaskCompletionSource<object>> _userCompletionSources;
        private readonly Dictionary<int, Avatar> _avatars;
        private readonly Dictionary<string, World> _worlds;
        private readonly Dictionary<int, User> _users;
        private TaskCompletionSource<object> _connectCompletionSource;
        private TaskCompletionSource<object> _loginCompletionSource;
        private TaskCompletionSource<object> _enterCompletionSource;
        private NetConfig _netConfig;
        private GCHandle _instanceHandle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VirtualParadiseClient" /> class.
        /// </summary>
        public VirtualParadiseClient()
        {
            Configuration = new VirtualParadiseClientConfiguration();
            _objectCompletionSources = new Dictionary<int, TaskCompletionSource<object>>();
            _userCompletionSources = new Dictionary<int, TaskCompletionSource<object>>();
            _worlds = new Dictionary<string, World>();
            _avatars = new Dictionary<int, Avatar>();
            _users = new Dictionary<int, User>();
            
            InitOnce();
            InitVpNative();
        }

        /// <summary>
        ///     Occurs when a chat message has been received.
        /// </summary>
        public event VpEventHandler<ChatMessageEventArgs> ChatMessageReceived;
        
        /// <summary>
        ///     Occurs when an avatar has entered the vicinity of this bot.
        /// </summary>
        public event VpEventHandler<AvatarEnterEventArgs> AvatarEntered;
        
        /// <summary>
        ///     Occurs when an avatar changed its state.
        /// </summary>
        public event VpEventHandler<AvatarChangeEventArgs> AvatarChanged;
        
        /// <summary>
        ///     Occurs when an avatar has left the vicinity of this bot.
        /// </summary>
        public event VpEventHandler<AvatarLeaveEventArgs> AvatarLeft;
        
        /// <summary>
        ///     Occurs when an avatar has been clicked.
        /// </summary>
        public event VpEventHandler<AvatarClickEventArgs> AvatarClicked;
        
        /// <summary>
        ///     Occurs when a join request has been received. 
        /// </summary>
        public event VpEventHandler<JoinEventArgs> JoinRequestReceived;

        /// <summary>
        ///     Occurs when this bot has been teleported.
        /// </summary>
        public event VpEventHandler<TeleportEventArgs> Teleported;

        /// <summary>
        ///     Occurs when an object has been created.
        /// </summary>
        public event VpEventHandler<ObjectCreateArgs> ObjectCreated;
        
        /// <summary>
        ///     Occurs when an object has been changed.
        /// </summary>
        public event VpEventHandler<ObjectChangeArgs> ObjectChanged;
        
        /// <summary>
        ///     Occurs when an object has been deleted.
        /// </summary>
        public event VpEventHandler<ObjectDeleteArgs> ObjectDeleted;
        
        /// <summary>
        ///     Occurs when an object has been clicked.
        /// </summary>
        /// <remarks>This event occurs whether or not the action of the object contains an <c>activate</c> trigger.</remarks>
        public event VpEventHandler<ObjectClickArgs> ObjectClicked;
        
        /// <summary>
        ///     Occurs when an avatar has collided with an object.
        /// </summary>
        /// <remarks>This event occurs whether or not the action of the object contains a <c>bump</c> trigger.</remarks>
        public event VpEventHandler<ObjectBumpArgs> ObjectBumped;
        
        /// <summary>
        ///     Occurs when a world list entry has been received as a result of calling <see cref="ListWorlds" />.
        /// </summary>
        public event VpEventHandler<WorldListEventArgs> WorldListEntryReceived;
        
        /// <summary>
        ///     Occurs when the world settings have been changed.
        /// </summary>
        public event VpEventHandler<WorldSettingsChangedEventArgs> WorldSettingsChanged;
        
        /// <summary>
        ///     Occurs when a friend has been added.
        /// </summary>
        public event VpEventHandler<FriendAddCallbackEventArgs> FriendAdded;
        
        /// <summary>
        ///     Occurs when a friend has been deleted.
        /// </summary>
        public event VpEventHandler<FriendDeleteCallbackEventArgs> FriendDeleted; 
        
        /// <summary>
        ///     Occurs when information about a friend has been received.
        /// </summary>
        public event VpEventHandler<FriendsGetCallbackEventArgs> FriendReceived;

        /// <summary>
        ///     Occurs when the client has been disconnected from the world server.
        /// </summary>
        public event VpEventHandler<WorldDisconnectEventArgs> WorldDisconnected;
        
        /// <summary>
        ///     Occurs when the client has been disconnected from the universe server.
        /// </summary>
        public event VpEventHandler<UniverseDisconnectEventArgs> UniverseDisconnected;

        /// <summary>
        ///     Occurs when object data has been received as a result of calling <see cref="QueryCell(int,int)" />.
        /// </summary>
        public event VpEventHandler<QueryCellResultArgs> QueryCellResult;
        
        /// <summary>
        ///     Occurs when all objects within a cell have been fetched as a result of calling <see cref="QueryCell(int,int)" />.
        /// </summary>
        public event VpEventHandler<QueryCellEndArgs> QueryCellEnd;

        /// <summary>
        ///     Occurs when this bot has entered a world.
        /// </summary>
        /// <remarks>
        ///     This event is not raised by the native SDK; it is only raised by <see cref="VirtualParadiseClient" />.
        /// </remarks>
        public event VpEventHandler<WorldEnterEventArgs> WorldEntered;
        
        /// <summary>
        ///     Occurs when this bot has left a world.
        /// </summary>
        /// <remarks>
        ///     This event is not raised by the native SDK; it is only raised by <see cref="VirtualParadiseClient" />.
        /// </remarks>
        public event VpEventHandler<WorldLeaveEventArgs> WorldLeft;

        /// <summary>
        ///     Gets a read-only view of the avatars currently seen by this instance.
        /// </summary>
        /// <value>A read-only view of the avatars currently seen by this instance.</value>
        public IReadOnlyCollection<Avatar> Avatars => _avatars.Values;

        /// <summary>
        ///     Gets or sets the configuration for this instance.
        /// </summary>
        /// <value>The configuration for this instance.</value>
        public VirtualParadiseClientConfiguration Configuration { get; set; }

        /// <summary>
        ///     Gets the current avatar for this instance, if any.
        /// </summary>
        /// <value>
        ///     An <see cref="Avatar" /> encapsulating the state of the avatar for this instance, or <see langword="null" /> if
        ///     this instance is not in a world.
        /// </value>
        public Avatar CurrentAvatar
        {
            get
            {
                if (World is null) return null;

                Avatar avatar;
                
                lock (this)
                {
                    int type = Functions.vp_int(NativeInstanceHandle, IntegerAttribute.MyType);
                    string name = Configuration.BotName;

                    double x = Functions.vp_double(NativeInstanceHandle, FloatAttribute.MyX);
                    double y = Functions.vp_double(NativeInstanceHandle, FloatAttribute.MyY);
                    double z = Functions.vp_double(NativeInstanceHandle, FloatAttribute.MyZ);

                    double pitch = Functions.vp_double(NativeInstanceHandle, FloatAttribute.MyPitch);
                    double yaw = Functions.vp_double(NativeInstanceHandle, FloatAttribute.MyYaw);

                    var position = new Vector3(x, y, z);
                    var rotation = new Vector3(pitch, yaw, 0);
                    var location = new Location(World, position, rotation);

                    avatar = new Avatar
                    {
                        Session = 0,
                        Name = name,
                        AvatarType = type,
                        Location = location,
                        LastChanged = DateTimeOffset.Now,
                        ApplicationName = Configuration.ApplicationName,
                        ApplicationVersion = Configuration.ApplicationVersion,
                        User = CurrentUser
                    };
                }

                return avatar;
            }
        }

        /// <summary>
        ///     Gets the user associated with the current instance.
        /// </summary>
        /// <value>The user associated with the current instance.</value>
        public User CurrentUser { get; private set; }

        /// <summary>
        ///     Gets the details about a user with a specific ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A <see cref="User" /> containing details about the specified user.</returns>
        public async Task<User> GetUserAsync(int userId)
        {
            if (_users.TryGetValue(userId, out User user))
                return user;
            
            var taskCompletionSource = new TaskCompletionSource<object>();

            lock (this)
            {
                _userCompletionSources.Add(userId, taskCompletionSource);
                // Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);
                // (may be necessary if the native SDK is ever refactored to use a callback rather than event)
                int rc = Functions.vp_user_attributes_by_id(NativeInstanceHandle, userId);
                if (rc != 0)
                {
                    _userCompletionSources.Remove(userId);
                    throw new VpException((ReasonCode) rc);
                }
            }

            user = (User) await taskCompletionSource.Task.ConfigureAwait(false);
            
            if (_users.ContainsKey(userId)) _users[userId] = user;
            else _users.Add(userId, user);

            _userCompletionSources.Remove(userId);
            return user;
        }

        /// <summary>
        ///     Gets the universe to which this instance is currently connected.
        /// </summary>
        /// <value>The universe to which this instance is currently connected.</value>
        public Universe Universe { get; private set; }

        /// <summary>
        ///     Gets the world to which this instance is currently connected.
        /// </summary>
        /// <value>The world to which this instance is currently connected.</value>
        public World World { get; private set; }
        
        internal IntPtr NativeInstanceHandle { get; private set; }

        internal void InitOnce()
        {
            _instanceHandle = GCHandle.Alloc(this);
            _netConfig.Context = GCHandle.ToIntPtr(_instanceHandle);
            _netConfig.Create = Connection.CreateNative;
            _netConfig.Destroy = Connection.DestroyNative;
            _netConfig.Connect = Connection.ConnectNative;
            _netConfig.Receive = Connection.ReceiveNative;
            _netConfig.Send = Connection.SendNative;
            _netConfig.Timeout = Connection.TimeoutNative;
        }

        private void InitVpNative()
        {
               
            int rc = Functions.vp_init(5);
            if (rc != 0)
            {
                throw new VpException((ReasonCode)rc);
            }

            NativeInstanceHandle = Functions.vp_create(ref _netConfig);

            SetNativeEvent(Events.Chat, OnChatNative);
            SetNativeEvent(Events.AvatarAdd, OnAvatarAddNative);
            SetNativeEvent(Events.AvatarChange, OnAvatarChangeNative);
            SetNativeEvent(Events.AvatarDelete, OnAvatarDeleteNative);
            SetNativeEvent(Events.AvatarClick, OnAvatarClickNative);
            SetNativeEvent(Events.WorldList, OnWorldListNative);
            SetNativeEvent(Events.WorldSetting, OnWorldSettingNative);
            SetNativeEvent(Events.WorldSettingsChanged, OnWorldSettingsChangedNative);
            SetNativeEvent(Events.ObjectChange, OnObjectChangeNative);
            SetNativeEvent(Events.Object, OnObjectCreateNative);
            SetNativeEvent(Events.ObjectClick, OnObjectClickNative);
            SetNativeEvent(Events.ObjectBumpBegin, OnObjectBumpNative);
            SetNativeEvent(Events.ObjectBumpEnd, OnObjectBumpEndNative);
            SetNativeEvent(Events.ObjectDelete, OnObjectDeleteNative);
            SetNativeEvent(Events.QueryCellEnd, OnQueryCellEndNative);
            SetNativeEvent(Events.UniverseDisconnect, OnUniverseDisconnectNative);
            SetNativeEvent(Events.WorldDisconnect, OnWorldDisconnectNative);
            SetNativeEvent(Events.Teleport, OnTeleportNative);
            SetNativeEvent(Events.UserAttributes, OnUserAttributesNative);
            SetNativeEvent(Events.Join, OnJoinNative);
            SetNativeCallback(Callbacks.ObjectAdd, OnObjectCreateCallbackNative);
            SetNativeCallback(Callbacks.ObjectChange, OnObjectChangeCallbackNative);
            SetNativeCallback(Callbacks.ObjectDelete, OnObjectDeleteCallbackNative);
            SetNativeCallback(Callbacks.ObjectGet, OnObjectGetCallbackNative);
            SetNativeCallback(Callbacks.ObjectLoad, this.OnObjectLoadCallbackNative);
            SetNativeCallback(Callbacks.FriendAdd, OnFriendAddCallbackNative);
            SetNativeCallback(Callbacks.FriendDelete, OnFriendDeleteCallbackNative);
            SetNativeCallback(Callbacks.GetFriends, OnGetFriendsCallbackNative);
            SetNativeCallback(Callbacks.Login, OnLoginCallbackNative);
            SetNativeCallback(Callbacks.Enter, OnEnterCallbackNativeEvent);
            //SetNativeCallback(Callbacks.Join, OnJoinCallbackNativeEvent);
            SetNativeCallback(Callbacks.ConnectUniverse, OnConnectUniverseCallbackNative);
            //SetNativeCallback(Callbacks.WorldPermissionUserSet, OnWorldPermissionUserSetCallbackNative);
            //SetNativeCallback(Callbacks.WorldPermissionSessionSet, OnWorldPermissionSessionSetCallbackNative);
            //SetNativeCallback(Callbacks.WorldSettingSet, OnWorldSettingsSetCallbackNative);
        }

        private void SetCompletionResult(int referenceNumber, int rc, object result)
        {
            var tcs = _objectCompletionSources[referenceNumber];
            SetCompletionResult(tcs, rc, result);
        }

        private static void SetCompletionResult(TaskCompletionSource<object> tcs, int rc, object result)
        {
            if (rc != 0)
            {
                tcs.SetException(new VpException((ReasonCode)rc));
            }
            else
            {
                tcs.SetResult(result);
            }
        }

        internal static void CheckReasonCode(int rc)
        {
            if (rc != 0)
            {
                throw new VpException((ReasonCode)rc);
            }
        }

        /// <summary>
        ///     Establishes a connection to default Virtual Paradise universe.
        /// </summary>
        public Task ConnectAsync()
        {
            return ConnectAsync(DefaultUniverseHost, DefaultUniversePort);
        }

        /// <summary>
        ///     Establishes a connection to the universe at the specified remote endpoint.
        /// </summary>
        /// <param name="host">The remote host.</param>
        /// <param name="port">The remote port.</param>
        public Task ConnectAsync(string host, int port)
        {
            EndPoint remoteEP = IPAddress.TryParse(host, out var ipAddress)
                ? (EndPoint) new IPEndPoint(ipAddress, port)
                : new DnsEndPoint(host, port);
            
            return ConnectAsync(remoteEP);
        }

        /// <summary>
        ///     Establishes a connection to the universe at the specified remote endpoint.
        /// </summary>
        /// <param name="remoteEP">The remote endpoint of the universe.</param>
        public Task ConnectAsync(EndPoint remoteEP)
        {
            string host;
            int port;

            switch (remoteEP)
            {
                case null:
                    host = DefaultUniverseHost;
                    port = DefaultUniversePort;
                    remoteEP = new DnsEndPoint(host, port); // reconstruct endpoint for Universe ctor
                    break;
                    
                case IPEndPoint ip:
                    host = ip.Address.ToString();
                    port = ip.Port;
                    break;
                
                case DnsEndPoint dns:
                    host = dns.Host;
                    port = dns.Port;
                    break;
                
                default:
                    throw new ArgumentException("The specified remote endpoint is not supported.", nameof(remoteEP));
            }
            
            Universe = new Universe(remoteEP);

            lock (this)
            {
                _connectCompletionSource = new TaskCompletionSource<object>();
                int reason = Functions.vp_connect_universe(NativeInstanceHandle, host, port);
                if (reason != 0)
                {
                    return Task.FromException<VpException>(new VpException((ReasonCode) reason));
                }

                return _connectCompletionSource.Task;
            }
        }

        public virtual async Task LoginAndEnterAsync(bool announceAvatar = true)
        {
            await ConnectAsync();
            await LoginAsync();
            await EnterAsync();
            if (announceAvatar)
            {
                UpdateAvatar();
            }
        }

        public virtual async Task LoginAsync()
        {
            if (Configuration == null ||
                string.IsNullOrEmpty(Configuration.BotName) ||
                string.IsNullOrEmpty(Configuration.Password) ||
                string.IsNullOrEmpty(Configuration.UserName)
                )
            {
                throw new ArgumentException("Can't login because of Incomplete login configuration.");
            }

            await LoginAsync(Configuration.UserName, Configuration.Password, Configuration.BotName);
        }

        public virtual async Task LoginAsync(string username, string password, string botname)
        {
            lock (this)
            {
                Configuration.BotName = botname;
                Configuration.UserName = username;
                Configuration.Password = password;
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ApplicationName, Configuration.ApplicationName);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ApplicationVersion, Configuration.ApplicationVersion);

                _loginCompletionSource = new TaskCompletionSource<object>();
                var rc = Functions.vp_login(NativeInstanceHandle, username, password, botname);
                if (rc != 0)
                {
                    throw new VpException((ReasonCode)rc);
                }
            }

            await _loginCompletionSource.Task;

            int myUserId = Functions.vp_int(NativeInstanceHandle, IntegerAttribute.MyUserId);
            CurrentUser = await GetUserAsync(myUserId);
        }

        public virtual Task EnterAsync(string worldname)
        {
            return EnterAsync(new World { Name = worldname });
        }

        public virtual Task EnterAsync()
        {
            if (Configuration == null || Configuration.World == null || string.IsNullOrEmpty(Configuration.World.Name))
                throw new ArgumentException("Can't login because of Incomplete instance world configuration.");
            return EnterAsync(Configuration.World);
        }

        public virtual Task EnterAsync(World world)
        {
            lock (this)
            {
                Configuration.World = world;

                _enterCompletionSource = new TaskCompletionSource<object>();
                var rc = Functions.vp_enter(NativeInstanceHandle, world.Name);
                if (rc != 0)
                {
                    return Task.FromException(new VpException((ReasonCode)rc));
                }

                return _enterCompletionSource.Task;
            }
        }

        /// <summary>
        /// Leave the current world
        /// </summary>
        public virtual void Leave()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_leave(NativeInstanceHandle));
                WorldLeft?.Invoke(this, new WorldLeaveEventArgs(Configuration.World));
            }
        }

        public virtual void Disconnect()
        {
            _avatars.Clear();
            Functions.vp_destroy(NativeInstanceHandle);
            InitVpNative();
            WorldLeft?.Invoke(this, new WorldLeaveEventArgs(Configuration.World));
            UniverseDisconnected?.Invoke(this, new UniverseDisconnectEventArgs(Universe, DisconnectType.UserDisconnected));

            CurrentUser = null;
            Universe = null;
        }

        public virtual void ListWorlds()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_list(NativeInstanceHandle, 0));
            }
        }

        public virtual void QueryCell(int cellX, int cellZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_query_cell(NativeInstanceHandle, cellX, cellZ));
            }
        }

        public virtual void QueryCell(int cellX, int cellZ, int revision)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_query_cell_revision(NativeInstanceHandle, cellX, cellZ, revision));
            }
        }

        public void ClickObject(VpObject vpObject)
        {
            lock (this)
            {
                ClickObject(vpObject.Id);
            }
        }

        public void ClickObject(int objectId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, objectId, 0, 0, 0, 0));
            }
        }

        public void ClickObject(VpObject vpObject, Avatar avatar)
        {
            lock (this)
            {
                ClickObject(vpObject.Id, avatar.Session);
            }
        }

        public void ClickObject(VpObject vpObject, Avatar avatar, Vector3 worldHit)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, vpObject.Id, avatar.Session, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z));
            }
        }

        public void ClickObject(VpObject vpObject, Vector3 worldHit)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, vpObject.Id, 0, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z));
            }
        }

        public void ClickObject(int objectId,int toSession, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, objectId, toSession, (float)worldHitX, (float)worldHitY, (float)worldHitZ));
            }
        }

        public void ClickObject(int objectId, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, objectId, 0, (float)worldHitX, (float)worldHitY, (float)worldHitZ));
            }
        }

        public void ClickObject(int objectId, int toSession)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(NativeInstanceHandle, objectId, toSession, 0, 0, 0));
            }
        }

        public virtual Task DeleteObjectAsync(VpObject vpObject)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);

                int rc = Functions.vp_object_delete(NativeInstanceHandle,vpObject.Id);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }

            return tcs.Task;
        }

        public virtual async Task<int> LoadObjectAsync(VpObject vpObject)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectModel, vpObject.Model);
                Functions.SetData(NativeInstanceHandle, DataAttribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectType, vpObject.ObjectType);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectUserId, vpObject.Owner);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectTime, (int)vpObject.Time.ToUnixTimeSeconds());

                int rc = Functions.vp_object_load(NativeInstanceHandle);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }

            var id = (int)await tcs.Task.ConfigureAwait(false);
            vpObject.Id = id;

            return id;
        }

        public virtual async Task<int> AddObjectAsync(VpObject vpObject)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectModel, vpObject.Model);
                Functions.SetData(NativeInstanceHandle, DataAttribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectType, vpObject.ObjectType);

                int rc = Functions.vp_object_add(NativeInstanceHandle);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }

            var id = (int)await tcs.Task.ConfigureAwait(false);
            vpObject.Id = id;

            return id;
        }

        public virtual Task ChangeObjectAsync(VpObject vpObject)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(NativeInstanceHandle, StringAttribute.ObjectModel, vpObject.Model);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ObjectType, vpObject.ObjectType);

                int rc = Functions.vp_object_change(NativeInstanceHandle);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }


            return tcs.Task;
        }

        public virtual async Task<VpObject> GetObjectAsync(int id)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(NativeInstanceHandle, IntegerAttribute.ReferenceNumber, referenceNumber);
                var rc = Functions.vp_object_get(NativeInstanceHandle, id);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }

            var obj = (VpObject)await tcs.Task.ConfigureAwait(false);
            return obj;
        }

        /// <summary>
        ///     Teleports an avatar to a specified location.
        /// </summary>
        /// <param name="avatar">The avatar to teleport.</param>
        /// <param name="location">The target location of the teleport.</param>
        /// <exception cref="ArgumentNullException"><paramref name="avatar" /> is <see langword="null" />.</exception>
        public void Teleport(Avatar avatar, Location location)
        {
            if (avatar is null) throw new ArgumentNullException(nameof(avatar));
            
            lock (this)
            {
                Vector3 position = location.Position;
                Vector3 rotation = location.Rotation;

                float x = (float) position.X;
                float y = (float) position.Y;
                float z = (float) position.Z;
                float yaw = (float) rotation.Y;
                float pitch = (float) rotation.X;
                string worldName = location.World.Name;

                if (string.IsNullOrWhiteSpace(worldName))
                    worldName = string.Empty;

                int rc = Functions.vp_teleport_avatar(NativeInstanceHandle, avatar.Session, worldName, x, y, z, yaw, pitch);
                CheckReasonCode(rc);
            }
        }

        public virtual void UpdateAvatar(double x = 0.0f, double y = 0.0f, double z = 0.0f,double yaw = 0.0f, double pitch = 0.0f)
        {
            lock (this)
            {
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.MyX, x);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.MyY, y);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.MyZ, z);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.MyYaw, yaw);
                Functions.vp_double_set(NativeInstanceHandle, FloatAttribute.MyPitch, pitch);
                CheckReasonCode(Functions.vp_state_change(NativeInstanceHandle));

            }
        }

        public void UpdateAvatar(Vector3 position)
        {
            UpdateAvatar(position.X, position.Y, position.Z);
        }

        public void UpdateAvatar(Vector3 position, Vector3 rotation)
        {
            UpdateAvatar(position.X, position.Y, position.Z, rotation.X, rotation.Y);
        }

        public void AvatarClick(int session)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_avatar_click(NativeInstanceHandle, session));
            }
        }

        public void AvatarClick(Avatar avatar)
        {
            AvatarClick(avatar.Session);
        }

        public virtual void Say(string message)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_say(NativeInstanceHandle, message));
            }
        }

        public virtual void Say(string format, params object[] arg)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_say(NativeInstanceHandle, string.Format(format, arg)));
            }
        }

        public void ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_console_message(NativeInstanceHandle, targetSession, name, message, (int)effects, red, green, blue));
            }
        }

        public void ConsoleMessage(Avatar avatar, string name, string message, Color color, TextEffectTypes effects = 0)
        {
            ConsoleMessage(avatar.Session, name, message, effects, color.R, color.G, color.B);
        }

        public void ConsoleMessage(int targetSession, string name, string message, Color color, TextEffectTypes effects = 0)
        {
            ConsoleMessage(targetSession, name, message, effects, color.R, color.G, color.B);
        }

        public void ConsoleMessage(string name, string message, Color color, TextEffectTypes effects = 0)
        {
            ConsoleMessage(0, name, message, effects, color.R, color.G, color.B);
        }

        public void ConsoleMessage(string message, Color color, TextEffectTypes effects = 0)
        {
            ConsoleMessage(0, string.Empty, message, effects, color.R, color.G, color.B);
        }

        public void ConsoleMessage(string message)
        {
            ConsoleMessage(0, string.Empty, message, 0, 0, 0, 0);
        }

        public virtual void ConsoleMessage(Avatar avatar, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            ConsoleMessage(avatar.Session, name, message, effects, red, green, blue);
        }

        public virtual void UrlSendOverlay(Avatar avatar, string url)
        {
            UrlSendOverlay(avatar.Session, url);
        }

        public virtual void UrlSendOverlay(Avatar avatar, Uri url)
        {
            UrlSendOverlay(avatar.Session, url.AbsoluteUri);
        }

        public virtual void UrlSendOverlay(int avatarSession, string url)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_url_send(NativeInstanceHandle, avatarSession, url, (int)UrlTarget.UrlTargetOverlay));
            }
        }

        public virtual void UrlSendOverlay(int avatarSession, Uri url)
        {
            UrlSendOverlay(avatarSession, url.AbsoluteUri);
        }

        public virtual void UrlSend(Avatar avatar, string url)
        {
            UrlSend(avatar.Session, url);
        }

        public virtual void UrlSend(Avatar avatar, Uri url)
        {
            UrlSend(avatar.Session, url.AbsoluteUri);
        }

        public virtual void UrlSend(int avatarSession, string url)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_url_send(NativeInstanceHandle, avatarSession, url, (int)UrlTarget.UrlTargetBrowser));
            }
        }

        public virtual void UrlSend(int avatarSession, Uri url)
        {
            UrlSend(avatarSession, url.AbsoluteUri);
        }

        public virtual void Join(Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_join(NativeInstanceHandle, avatar.User.Id));
            }
        }

        public virtual void Join(int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_join(NativeInstanceHandle, userId));
            }
        }

        public virtual void WorldPermissionUser(string permission, int userId, int enable)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, permission, userId, enable));
            }
        }

        public virtual void WorldPermissionUserEnable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), avatar.User.Id, 1));
            }
        }

        public virtual void WorldPermissionUserEnable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), userId, 1));
            }
        }

        public virtual void WorldPermissionUserDisable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), avatar.User.Id, 0));
            }
        }

        public virtual void WorldPermissionUserDisable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), userId, 0));
            }
        }

        public virtual void WorldPermissionSession(string permission, int sessionId, int enable)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_session_set(NativeInstanceHandle, permission, sessionId, enable));
            }
        }

        public virtual void WorldPermissionSessionEnable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 1));
            }
        }

        public virtual void WorldPermissionSessionEnable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), session, 1));
            }
        }


        public virtual void WorldPermissionSessionDisable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 0));
            }
        }

        public virtual void WorldPermissionSessionDisable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(NativeInstanceHandle, Enum.GetName(typeof(WorldPermissions), permission), session, 0));
            }
        }

        public virtual void WorldSettingSession(string setting, string value, Avatar toAvatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_setting_set(NativeInstanceHandle, setting, value, toAvatar.Session));
            }
        }

        public virtual void WorldSettingSession(string setting, string value, int  toSession)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_setting_set(NativeInstanceHandle, setting, value, toSession));
            }
        }

        public Avatar GetAvatar(int session)
        {
            _avatars.TryGetValue(session, out Avatar avatar);
            return avatar;
        }

        private static void GetVpObject(IntPtr sender, out VpObject vpObject)
        {
            vpObject = new VpObject
            {
                Action = Functions.vp_string(sender, StringAttribute.ObjectAction),
                Description = Functions.vp_string(sender, StringAttribute.ObjectDescription),
                Id = Functions.vp_int(sender, IntegerAttribute.ObjectId),
                Model = Functions.vp_string(sender, StringAttribute.ObjectModel),
                Data = Functions.GetData(sender, DataAttribute.ObjectData),

                Rotation = new Vector3
                {
                    X = Functions.vp_double(sender, FloatAttribute.ObjectRotationX),
                    Y = Functions.vp_double(sender, FloatAttribute.ObjectRotationY),
                    Z = Functions.vp_double(sender, FloatAttribute.ObjectRotationZ)
                },

                Time = DateTimeOffset.FromUnixTimeSeconds(Functions.vp_int(sender, IntegerAttribute.ObjectTime)).UtcDateTime,
                ObjectType = Functions.vp_int(sender, IntegerAttribute.ObjectType),
                Owner = Functions.vp_int(sender, IntegerAttribute.ObjectUserId),
                Position = new Vector3
                {
                    X = Functions.vp_double(sender, FloatAttribute.ObjectX),
                    Y = Functions.vp_double(sender, FloatAttribute.ObjectY),
                    Z = Functions.vp_double(sender, FloatAttribute.ObjectZ)
                },
                Angle = Functions.vp_double(sender, FloatAttribute.ObjectRotationAngle)
            };
        }

        public void ReleaseEvents()
        {
            lock (this)
            {
                ChatMessageReceived = null;
                AvatarEntered = null;
                AvatarChanged = null;
                AvatarLeft = null;
                ObjectCreated = null;
                ObjectChanged = null;
                ObjectDeleted = null;
                ObjectClicked = null;
                ObjectBumped = null;
                WorldListEntryReceived = null;
                WorldDisconnected = null;
                WorldSettingsChanged = null;
                WorldDisconnected = null;
                UniverseDisconnected = null;
                QueryCellResult = null;
                QueryCellEnd = null;
                FriendAdded = null;
                FriendReceived = null;
            }
        }

        public void Dispose()
        {
            if (NativeInstanceHandle != IntPtr.Zero)
            {
                Functions.vp_destroy(NativeInstanceHandle);
            }
            
            if (_instanceHandle != GCHandle.FromIntPtr(IntPtr.Zero))
            {
                _instanceHandle.Free();
            }

            GC.SuppressFinalize(this);
        }

        public void GetFriends()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friends_get(NativeInstanceHandle));
            }
        }

        public void AddFriendByName(Friend friend)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_add_by_name(NativeInstanceHandle, friend.Name));
            }
        }

        public void AddFriendByName(string name)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_add_by_name(NativeInstanceHandle, name));
            }
        }

        public void DeleteFriendById(int friendId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_delete(NativeInstanceHandle, friendId));
            }
        }

        public void DeleteFriendById(Friend friend)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_delete(NativeInstanceHandle, friend.UserId));
            }
        }

        public void TerrianQuery(int tileX, int tileZ, int[,] nodes)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_terrain_query(NativeInstanceHandle, tileX, tileZ, nodes));
            }
        }

        public void SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_terrain_node_set(NativeInstanceHandle, tileX, tileZ, nodeX, nodeZ, cells));
            }
        }
    }
}
