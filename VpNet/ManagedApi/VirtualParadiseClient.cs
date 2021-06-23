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
        private readonly Dictionary<int, Avatar> _avatars;
        private readonly Dictionary<string, World> _worlds;
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
            _worlds = new Dictionary<string, World>();
            _avatars = new Dictionary<int, Avatar>();
            InitOnce();
            InitVpNative();
        }

        public event VpEventHandler<ChatMessageEventArgs> OnChatMessage;
        public event VpEventHandler<AvatarEnterEventArgs> OnAvatarEnter;
        public event VpEventHandler<AvatarChangeEventArgs> OnAvatarChange;
        public event VpEventHandler<AvatarLeaveEventArgs> OnAvatarLeave;
        public event VpEventHandler<AvatarClickEventArgs> OnAvatarClick;
        public event VpEventHandler<JoinEventArgs> OnJoin;

        public event VpEventHandler<TeleportEventArgs> OnTeleport;
        public event VpEventHandler<UserAttributesEventArgs> OnUserAttributes;

        public event VpEventHandler<ObjectCreateArgs> OnObjectCreate;
        public event VpEventHandler<ObjectChangeArgs> OnObjectChange;
        public event VpEventHandler<ObjectDeleteArgs> OnObjectDelete;
        public event VpEventHandler<ObjectClickArgs> OnObjectClick;
        public event VpEventHandler<ObjectBumpArgs> OnObjectBump;


        public event VpEventHandler<WorldListEventArgs> OnWorldList;
        public event VpEventHandler<WorldSettingsChangedEventArgs> OnWorldSettingsChanged;
        public event VpEventHandler<FriendAddCallbackEventArgs> OnFriendAddCallback;
        public event VpEventHandler<FriendDeleteCallbackEventArgs> OnFriendDeleteCallback; 
        public event VpEventHandler<FriendsGetCallbackEventArgs> OnFriendsGetCallback;

        public event VpEventHandler<WorldDisconnectEventArgs> OnWorldDisconnect;
        public event VpEventHandler<UniverseDisconnectEventArgs> OnUniverseDisconnect;

        public event VpEventHandler<QueryCellResultArgs> OnQueryCellResult;
        public event VpEventHandler<QueryCellEndArgs> OnQueryCellEnd;

        public event VpEventHandler<WorldEnterEventArgs> OnWorldEnter;
        
        public event VpEventHandler<WorldLeaveEventArgs> OnWorldLeave;
        
        
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
   
        /// <summary>
        ///     Gets or sets the configuration for this instance.
        /// </summary>
        /// <value>The configuration for this instance.</value>
        public VirtualParadiseClientConfiguration Configuration { get; set; }

        /// <summary>
        ///     Gets a read-only view of the avatars currently seen by this instance.
        /// </summary>
        /// <value>A read-only view of the avatars currently seen by this instance.</value>
        public IReadOnlyCollection<Avatar> Avatars => _avatars.Values;

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
        
        internal IntPtr InternalInstance { get; private set; }

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

            OnChatNativeEvent += OnChatNative;
            OnAvatarAddNativeEvent += OnAvatarAddNative;
            OnAvatarChangeNativeEvent += OnAvatarChangeNative;
            OnAvatarDeleteNativeEvent += OnAvatarDeleteNative;
            OnAvatarClickNativeEvent += OnAvatarClickNative;
            OnWorldListNativeEvent += OnWorldListNative;
            OnWorldDisconnectNativeEvent += OnWorldDisconnectNative;

            OnObjectChangeNativeEvent += OnObjectChangeNative;
            OnObjectCreateNativeEvent += OnObjectCreateNative;
            OnObjectClickNativeEvent += OnObjectClickNative;
            OnObjectBumpNativeEvent += OnObjectBumpNative;
            OnObjectBumpEndNativeEvent += OnObjectBumpEndNative;
            OnObjectDeleteNativeEvent += OnObjectDeleteNative;

            OnQueryCellEndNativeEvent += OnQueryCellEndNative;
            OnUniverseDisconnectNativeEvent += OnUniverseDisconnectNative;
            OnTeleportNativeEvent += OnTeleportNative;
            OnUserAttributesNativeEvent += OnUserAttributesNative;
            OnJoinNativeEvent += OnJoinNative;

            OnObjectCreateCallbackNativeEvent += OnObjectCreateCallbackNative;
            OnObjectChangeCallbackNativeEvent += OnObjectChangeCallbackNative;
            OnObjectDeleteCallbackNativeEvent += OnObjectDeleteCallbackNative;
            OnObjectGetCallbackNativeEvent += OnObjectGetCallbackNative;
            this.OnObjectLoadCallbackNativeEvent += this.OnObjectLoadCallbackNative;

            OnFriendAddCallbackNativeEvent += OnFriendAddCallbackNative;
            OnFriendDeleteCallbackNativeEvent += OnFriendDeleteCallbackNative;
            OnGetFriendsCallbackNativeEvent += OnGetFriendsCallbackNative;
        }

        private void InitVpNative()
        {
               
            int rc = Functions.vp_init(5);
            if (rc != 0)
            {
                throw new VpException((ReasonCode)rc);
            }

            InternalInstance = Functions.vp_create(ref _netConfig);

            SetNativeEvent(Events.Chat, OnChatNative1);
            SetNativeEvent(Events.AvatarAdd, OnAvatarAddNative1);
            SetNativeEvent(Events.AvatarChange, OnAvatarChangeNative1);
            SetNativeEvent(Events.AvatarDelete, OnAvatarDeleteNative1);
            SetNativeEvent(Events.AvatarClick, OnAvatarClickNative1);
            SetNativeEvent(Events.WorldList, OnWorldListNative1);
            SetNativeEvent(Events.WorldSetting, OnWorldSettingNative1);
            SetNativeEvent(Events.WorldSettingsChanged, OnWorldSettingsChangedNative1);
            SetNativeEvent(Events.ObjectChange, OnObjectChangeNative1);
            SetNativeEvent(Events.Object, OnObjectCreateNative1);
            SetNativeEvent(Events.ObjectClick, OnObjectClickNative1);
            SetNativeEvent(Events.ObjectBumpBegin, OnObjectBumpNative1);
            SetNativeEvent(Events.ObjectBumpEnd, OnObjectBumpEndNative1);
            SetNativeEvent(Events.ObjectDelete, OnObjectDeleteNative1);
            SetNativeEvent(Events.QueryCellEnd, OnQueryCellEndNative1);
            SetNativeEvent(Events.UniverseDisconnect, OnUniverseDisconnectNative1);
            SetNativeEvent(Events.WorldDisconnect, OnWorldDisconnectNative1);
            SetNativeEvent(Events.Teleport, OnTeleportNative1);
            SetNativeEvent(Events.UserAttributes, OnUserAttributesNative1);
            SetNativeEvent(Events.Join, OnJoinNative1);
            SetNativeCallback(Callbacks.ObjectAdd, OnObjectCreateCallbackNative1);
            SetNativeCallback(Callbacks.ObjectChange, OnObjectChangeCallbackNative1);
            SetNativeCallback(Callbacks.ObjectDelete, OnObjectDeleteCallbackNative1);
            SetNativeCallback(Callbacks.ObjectGet, OnObjectGetCallbackNative1);
            SetNativeCallback(Callbacks.ObjectLoad, this.OnObjectLoadCallbackNative1);
            SetNativeCallback(Callbacks.FriendAdd, OnFriendAddCallbackNative1);
            SetNativeCallback(Callbacks.FriendDelete, OnFriendDeleteCallbackNative1);
            SetNativeCallback(Callbacks.GetFriends, OnGetFriendsCallbackNative1);
            SetNativeCallback(Callbacks.Login, OnLoginCallbackNative1);
            SetNativeCallback(Callbacks.Enter, OnEnterCallbackNativeEvent1);
            //SetNativeCallback(Callbacks.Join, OnJoinCallbackNativeEvent1);
            SetNativeCallback(Callbacks.ConnectUniverse, OnConnectUniverseCallbackNative1);
            //SetNativeCallback(Callbacks.WorldPermissionUserSet, OnWorldPermissionUserSetCallbackNative1);
            //SetNativeCallback(Callbacks.WorldPermissionSessionSet, OnWorldPermissionSessionSetCallbackNative1);
            //SetNativeCallback(Callbacks.WorldSettingSet, OnWorldSettingsSetCallbackNative1);
        }

        internal void OnObjectCreateCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectCreateCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectChangeCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectChangeCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectDeleteCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectDeleteCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectGetCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectGetCallbackNativeEvent(instance, rc, reference); } }
        internal void OnFriendAddCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendAddCallbackNativeEvent(instance, rc, reference); } }
        internal void OnFriendDeleteCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendDeleteCallbackNativeEvent(instance, rc, reference); } }
        internal void OnGetFriendsCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendDeleteCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectLoadCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectLoadCallbackNativeEvent(instance, rc, reference); } }
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
                OnWorldEnter?.Invoke(this, new WorldEnterEventArgs(World));
            }
        }
        internal void OnJoinCallbackNativeEvent1(IntPtr instance, int rc, int reference) { lock (this) { OnJoinCallbackNativeEvent(instance, rc, reference); } }
        internal void OnConnectUniverseCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResult(_connectCompletionSource, rc, null);
            }
        }
        internal void OnWorldPermissionUserSetCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnWorldPermissionUserSetCallbackNativeEvent(instance, rc, reference); } }
        internal void OnWorldPermissionSessionSetCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnWorldPermissionSessionSetCallbackNativeEvent(instance, rc, reference); } }
        internal void OnWorldSettingsSetCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnWorldSettingsSetCallbackNativeEvent(instance, rc, reference); } }



        internal void OnChatNative1(IntPtr instance) { lock (this) { OnChatNativeEvent(instance); } }
        internal void OnAvatarAddNative1(IntPtr instance) { lock (this) { OnAvatarAddNativeEvent(instance); } }
        internal void OnAvatarChangeNative1(IntPtr instance) { lock (this) { OnAvatarChangeNativeEvent(instance); } }
        internal void OnAvatarDeleteNative1(IntPtr instance) { lock (this) { OnAvatarDeleteNativeEvent(instance); } }
        internal void OnAvatarClickNative1(IntPtr instance) { lock (this) { OnAvatarClickNativeEvent(instance); } }
        internal void OnWorldListNative1(IntPtr instance) { lock (this) { OnWorldListNativeEvent(instance); } }
        internal void OnWorldDisconnectNative1(IntPtr instance) { lock (this) { OnWorldDisconnectNativeEvent(instance); } }
        internal void OnWorldSettingsChangedNative1(IntPtr instance) { lock (this) { OnWorldSettingsChangedNativeEvent(instance); } }
        internal void OnWorldSettingNative1(IntPtr instance) { lock (this) { OnWorldSettingNativeEvent(instance); } }
        internal void OnObjectChangeNative1(IntPtr instance) { lock (this) { OnObjectChangeNativeEvent(instance); } }
        internal void OnObjectCreateNative1(IntPtr instance) { lock (this) { OnObjectCreateNativeEvent(instance); } }
        internal void OnObjectClickNative1(IntPtr instance) { lock (this) { OnObjectClickNativeEvent(instance); } }
        internal void OnObjectBumpNative1(IntPtr instance) { lock (this) { OnObjectBumpNativeEvent(instance); } }
        internal void OnObjectBumpEndNative1(IntPtr instance) { lock (this) { OnObjectBumpEndNativeEvent(instance); } }
        internal void OnObjectDeleteNative1(IntPtr instance) { lock (this) { OnObjectDeleteNativeEvent(instance); } }
        internal void OnQueryCellEndNative1(IntPtr instance) { lock (this) { OnQueryCellEndNativeEvent(instance); } }
        internal void OnUniverseDisconnectNative1(IntPtr instance) { lock (this) { OnUniverseDisconnectNativeEvent(instance); } }
        internal void OnTeleportNative1(IntPtr instance) { lock (this) { OnTeleportNativeEvent(instance); } }
        internal void OnUserAttributesNative1(IntPtr instance) { lock (this){OnUserAttributesNativeEvent(instance);} }
        internal void OnJoinNative1(IntPtr instance) { lock (this) { OnJoinNativeEvent(instance); } }

        #region Methods

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

        #region IUniverseFunctions Implementations

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
                int reason = Functions.vp_connect_universe(InternalInstance, host, port);
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

        public virtual Task LoginAsync(string username, string password, string botname)
        {
            lock (this)
            {
                Configuration.BotName = botname;
                Configuration.UserName = username;
                Configuration.Password = password;
                Functions.vp_string_set(InternalInstance, StringAttribute.ApplicationName, Configuration.ApplicationName);
                Functions.vp_string_set(InternalInstance, StringAttribute.ApplicationVersion, Configuration.ApplicationVersion);

                _loginCompletionSource = new TaskCompletionSource<object>();
                var rc = Functions.vp_login(InternalInstance, username, password, botname);
                if (rc != 0)
                {
                    return Task.FromException(new VpException((ReasonCode)rc));
                }

                return _loginCompletionSource.Task;
            }
        }

        #endregion

        #region WorldFunctions Implementations

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
                var rc = Functions.vp_enter(InternalInstance, world.Name);
                if (rc != 0)
                {
                    return Task.FromException(new VpException((ReasonCode)rc));
                }

                return _enterCompletionSource.Task;
            }
        }

        /// <summary>
        ///     If connected to a world, returns the state of this instance's avatar.
        /// </summary>
        /// <returns>An instance of <see cref="Avatar" />, encapsulating the state of this instance.</returns>
        public Avatar My()
        {
            Avatar avatar;

            if (World is null) return null;
            
            lock (this)
            {
                int userId = Functions.vp_int(InternalInstance, IntegerAttribute.MyUserId);
                int type = Functions.vp_int(InternalInstance, IntegerAttribute.MyType);
                string name = Configuration.BotName;

                double x = Functions.vp_double(InternalInstance, FloatAttribute.MyX);
                double y = Functions.vp_double(InternalInstance, FloatAttribute.MyY);
                double z = Functions.vp_double(InternalInstance, FloatAttribute.MyZ);
                
                double pitch = Functions.vp_double(InternalInstance, FloatAttribute.MyPitch);
                double yaw = Functions.vp_double(InternalInstance, FloatAttribute.MyYaw);

                var position = new Vector3(x, y, z);
                var rotation = new Vector3(pitch, yaw, 0);

                avatar = new Avatar(userId, 0, name, type, position, rotation, DateTimeOffset.Now,
                    Configuration.ApplicationName, Configuration.ApplicationVersion);
            }

            return avatar;
        }

        /// <summary>
        /// Leave the current world
        /// </summary>
        public virtual void Leave()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_leave(InternalInstance));
                OnWorldLeave?.Invoke(this, new WorldLeaveEventArgs(Configuration.World));
            }
        }

        public virtual void Disconnect()
        {
            _avatars.Clear();
            Functions.vp_destroy(InternalInstance);
            InitVpNative();
            OnUniverseDisconnect?.Invoke(this, new UniverseDisconnectEventArgs(Universe, DisconnectType.UserDisconnected));

            Universe = null;
        }

        public virtual void ListWorlds()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_list(InternalInstance, 0));
            }
        }

        #endregion

        #region IQueryCellFunctions Implementation

        public virtual void QueryCell(int cellX, int cellZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_query_cell(InternalInstance, cellX, cellZ));
            }
        }

        public virtual void QueryCell(int cellX, int cellZ, int revision)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_query_cell_revision(InternalInstance, cellX, cellZ, revision));
            }
        }

        #endregion

        #region VpObjectFunctions implementations

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
                CheckReasonCode(Functions.vp_object_click(InternalInstance, objectId, 0, 0, 0, 0));
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
                CheckReasonCode(Functions.vp_object_click(InternalInstance, vpObject.Id, avatar.Session, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z));
            }
        }

        public void ClickObject(VpObject vpObject, Vector3 worldHit)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(InternalInstance, vpObject.Id, 0, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z));
            }
        }

        public void ClickObject(int objectId,int toSession, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(InternalInstance, objectId, toSession, (float)worldHitX, (float)worldHitY, (float)worldHitZ));
            }
        }

        public void ClickObject(int objectId, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(InternalInstance, objectId, 0, (float)worldHitX, (float)worldHitY, (float)worldHitZ));
            }
        }

        public void ClickObject(int objectId, int toSession)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_object_click(InternalInstance, objectId, toSession, 0, 0, 0));
            }
        }

        public virtual Task DeleteObjectAsync(VpObject vpObject)
        {
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            var tcs = new TaskCompletionSource<object>();
            lock (this)
            {
                _objectCompletionSources.Add(referenceNumber, tcs);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ReferenceNumber, referenceNumber);

                int rc = Functions.vp_object_delete(InternalInstance,vpObject.Id);
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
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectModel, vpObject.Model);
                Functions.SetData(InternalInstance, DataAttribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectType, vpObject.ObjectType);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectUserId, vpObject.Owner);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectTime, (int)vpObject.Time.ToUnixTimeSeconds());

                int rc = Functions.vp_object_load(InternalInstance);
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
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectModel, vpObject.Model);
                Functions.SetData(InternalInstance, DataAttribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectType, vpObject.ObjectType);

                int rc = Functions.vp_object_add(InternalInstance);
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
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(InternalInstance, StringAttribute.ObjectModel, vpObject.Model);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ObjectType, vpObject.ObjectType);

                int rc = Functions.vp_object_change(InternalInstance);
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
                Functions.vp_int_set(InternalInstance, IntegerAttribute.ReferenceNumber, referenceNumber);
                var rc = Functions.vp_object_get(InternalInstance, id);
                if (rc != 0)
                {
                    _objectCompletionSources.Remove(referenceNumber);
                    throw new VpException((ReasonCode)rc);
                }
            }

            var obj = (VpObject)await tcs.Task.ConfigureAwait(false);
            return obj;
        }

        #endregion

        #region ITeleportFunctions Implementations

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

                int rc = Functions.vp_teleport_avatar(InternalInstance, avatar.Session, worldName, x, y, z, yaw, pitch);
                CheckReasonCode(rc);
            }
        }

        #endregion

        #region AvatarFunctions Implementations.

        public virtual void GetUserProfile(int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_user_attributes_by_id(InternalInstance, userId));
            }
        }

        [Obsolete]
        public virtual void GetUserProfile(string userName)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_user_attributes_by_name(InternalInstance, userName));
            }
        }

        public virtual void GetUserProfile(Avatar profile)
        {
            GetUserProfile(profile.UserId);
        }

        public virtual void UpdateAvatar(double x = 0.0f, double y = 0.0f, double z = 0.0f,double yaw = 0.0f, double pitch = 0.0f)
        {
            lock (this)
            {
                Functions.vp_double_set(InternalInstance, FloatAttribute.MyX, x);
                Functions.vp_double_set(InternalInstance, FloatAttribute.MyY, y);
                Functions.vp_double_set(InternalInstance, FloatAttribute.MyZ, z);
                Functions.vp_double_set(InternalInstance, FloatAttribute.MyYaw, yaw);
                Functions.vp_double_set(InternalInstance, FloatAttribute.MyPitch, pitch);
                CheckReasonCode(Functions.vp_state_change(InternalInstance));

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
                CheckReasonCode(Functions.vp_avatar_click(InternalInstance, session));
            }
        }

        public void AvatarClick(Avatar avatar)
        {
            AvatarClick(avatar.Session);
        }

        #endregion

        #region IChatFunctions Implementations

        public virtual void Say(string message)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_say(InternalInstance, message));
            }
        }

        public virtual void Say(string format, params object[] arg)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_say(InternalInstance, string.Format(format, arg)));
            }
        }

        public void ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_console_message(InternalInstance, targetSession, name, message, (int)effects, red, green, blue));
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
                CheckReasonCode(Functions.vp_url_send(InternalInstance, avatarSession, url, (int)UrlTarget.UrlTargetOverlay));
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
                CheckReasonCode(Functions.vp_url_send(InternalInstance, avatarSession, url, (int)UrlTarget.UrlTargetBrowser));
            }
        }

        public virtual void UrlSend(int avatarSession, Uri url)
        {
            UrlSend(avatarSession, url.AbsoluteUri);
        }

        #endregion

        #region IJoinFunctions Implementations
        public virtual void Join(Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_join(InternalInstance, avatar.UserId));
            }
        }

        public virtual void Join(int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_join(InternalInstance, userId));
            }
        }

        #endregion

        #region  WorldPermissionFunctions Implementations

        public virtual void WorldPermissionUser(string permission, int userId, int enable)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, permission, userId, enable));
            }
        }

        public virtual void WorldPermissionUserEnable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission),avatar.UserId,1));
            }
        }

        public virtual void WorldPermissionUserEnable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), userId, 1));
            }
        }

        public virtual void WorldPermissionUserDisable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), avatar.UserId, 0));
            }
        }

        public virtual void WorldPermissionUserDisable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), userId, 0));
            }
        }

        public virtual void WorldPermissionSession(string permission, int sessionId, int enable)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_session_set(InternalInstance, permission, sessionId, enable));
            }
        }

        public virtual void WorldPermissionSessionEnable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 1));
            }
        }

        public virtual void WorldPermissionSessionEnable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), session, 1));
            }
        }


        public virtual void WorldPermissionSessionDisable(WorldPermissions permission, Avatar avatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 0));
            }
        }

        public virtual void WorldPermissionSessionDisable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_permission_user_set(InternalInstance, Enum.GetName(typeof(WorldPermissions), permission), session, 0));
            }
        }

        #endregion

        #region WorldSettingsFunctions Implementations

        public virtual void WorldSettingSession(string setting, string value, Avatar toAvatar)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_setting_set(InternalInstance, setting, value, toAvatar.Session));
            }
        }

        public virtual void WorldSettingSession(string setting, string value, int  toSession)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_world_setting_set(InternalInstance, setting, value, toSession));
            }
        }

        #endregion

        #endregion

        #region Events

        private readonly Dictionary<Events, EventDelegate> _nativeEvents = new Dictionary<Events, EventDelegate>();
        private readonly Dictionary<Callbacks, CallbackDelegate> _nativeCallbacks = new Dictionary<Callbacks, CallbackDelegate>();





        private void SetNativeEvent(Events eventType, EventDelegate eventFunction)
        {
            _nativeEvents[eventType] = eventFunction;
            Functions.vp_event_set(InternalInstance, (int)eventType, eventFunction);
        }

        private void SetNativeCallback(Callbacks callbackType, CallbackDelegate callbackFunction)
        {
            _nativeCallbacks[callbackType] = callbackFunction;
            Functions.vp_callback_set(InternalInstance, (int)callbackType, callbackFunction);
        }

        #endregion

        #region CallbackHandlers

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

        void OnObjectGetCallbackNative(IntPtr sender, int rc, int reference)
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
        #endregion

        #region Event handlers

        private void OnUserAttributesNative(IntPtr sender)
        {
            if (OnUserAttributes == null)
                return;
            
            UserAttributes attributes;
            
            lock (this)
            {
                int id = Functions.vp_int(sender, IntegerAttribute.UserId);
                string name = Functions.vp_string(sender, StringAttribute.UserName);
                string email = Functions.vp_string(sender, StringAttribute.UserEmail);
                int lastLoginTimestamp = Functions.vp_int(sender, IntegerAttribute.UserLastLogin);
                int registrationDateTimestamp = Functions.vp_int(sender, IntegerAttribute.UserRegistrationTime);
                int onlineTimeSeconds = Functions.vp_int(sender, IntegerAttribute.UserOnlineTime);

                DateTimeOffset lastLogin = DateTimeOffset.FromUnixTimeSeconds(lastLoginTimestamp);
                DateTimeOffset registrationDate = DateTimeOffset.FromUnixTimeSeconds(registrationDateTimestamp);
                TimeSpan onlineTime = TimeSpan.FromSeconds(onlineTimeSeconds);

                attributes = new UserAttributes(id, name, email, lastLogin.UtcDateTime, onlineTime, registrationDate.UtcDateTime);
            }

            var args = new UserAttributesEventArgs(attributes);
            OnUserAttributes(this, args);
        }

        private void OnTeleportNative(IntPtr sender)
        {
            if (OnTeleport == null) return;
            
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
            
            Debug.Assert(!(OnTeleport is null), $"{nameof(OnTeleport)} != null");
            OnTeleport.Invoke(this, args);
        }

        private void OnGetFriendsCallbackNative(IntPtr sender, int rc, int reference)
        {
            if (OnFriendAddCallback == null)
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
            
            Debug.Assert(!(OnFriendsGetCallback is null), $"{nameof(OnFriendsGetCallback)} != null");
            OnFriendsGetCallback.Invoke(this, args);
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
            if (OnChatMessage is null)
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
                    byte r = (byte)Functions.vp_int(sender, IntegerAttribute.ChatColorRed);
                    byte g = (byte)Functions.vp_int(sender, IntegerAttribute.ChatColorGreen);
                    byte b = (byte)Functions.vp_int(sender, IntegerAttribute.ChatColorBlue);

                    color = new Color(r, g, b);
                }
                
                if (!_avatars.TryGetValue(session, out avatar))
                    _avatars.Add(session, avatar = new Avatar(0, session, name, 0, Vector3.Zero, Vector3.Zero, DateTimeOffset.Now, string.Empty, string.Empty));
                
                message = new ChatMessage(name, text, type, color, effects);
            }

            var args = new ChatMessageEventArgs(avatar, message);
            OnChatMessage.Invoke(this, args);
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

                avatar = new Avatar(userId, 0, name, type, position, rotation, DateTimeOffset.Now, applicationName, applicationVersion);

                if (_avatars.ContainsKey(session))
                    _avatars[session] = avatar;
                else
                    _avatars.Add(session, avatar);
            }
            
            if (OnAvatarEnter is null) return;

            var args = new AvatarEnterEventArgs(avatar);
            OnAvatarEnter?.Invoke(this, args);
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
                avatar.Position = new Vector3(x, y, z);
                avatar.Rotation = new Vector3(pitch, yaw, 0);
                avatar.LastChanged = DateTimeOffset.Now;
            }
            OnAvatarChange?.Invoke(this, new AvatarChangeEventArgs(avatar, oldAvatar));
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
            
            OnAvatarLeave?.Invoke(this, new AvatarLeaveEventArgs(avatar));
        }

        private void OnAvatarClickNative(IntPtr sender)
        {
            if (OnAvatarClick is null)
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
            
            Debug.Assert(!(OnAvatarClick is null), $"{nameof(OnAvatarClick)} != null");
            OnAvatarClick.Invoke(this, args);
        }

        private void OnObjectClickNative(IntPtr sender)
        {
            if (OnObjectClick is null)
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
            
            Debug.Assert(!(OnObjectClick is null), $"{nameof(OnObjectClick)} != null");
            OnObjectClick.Invoke(this, args);
        }

        private void OnObjectBumpNative(IntPtr sender)
        {
            if (OnObjectBump == null)
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

            Debug.Assert(!(OnObjectBump is null), $"{nameof(OnObjectBump)} != null");
            OnObjectBump.Invoke(this, args);
        }

        private void OnObjectBumpEndNative(IntPtr sender)
        {
            if (OnObjectBump == null)
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

            Debug.Assert(!(OnObjectBump is null), $"{nameof(OnObjectBump)} != null");
            OnObjectBump.Invoke(this, args);
        }

        private void OnObjectDeleteNative(IntPtr sender)
        {
            if (OnObjectDelete == null)
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
            
            Debug.Assert(!(OnObjectDelete is null), $"{nameof(OnObjectDelete)} != null");
            OnObjectDelete.Invoke(this, args);
        }

        private void OnObjectCreateNative(IntPtr sender)
        {
            if (OnObjectCreate is null && OnQueryCellResult is null)
                return;
            
            int session;
            
            lock (this)
            {
                session = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
            }
            
            var avatar = GetAvatar(session);
            
            GetVpObject(sender, out VpObject vpObject);
            
            if (session == 0)
                OnQueryCellResult?.Invoke(this, new QueryCellResultArgs(vpObject));
            else
                OnObjectCreate?.Invoke(this, new ObjectCreateArgs(avatar, vpObject));
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

        private void OnObjectChangeNative(IntPtr sender)
        {
            if (OnObjectChange == null) return;
            VpObject vpObject;
            int sessionId;
            lock (this)
            {
                GetVpObject(sender, out vpObject);
                sessionId = Functions.vp_int(sender, IntegerAttribute.AvatarSession);
            }
            OnObjectChange(this, new ObjectChangeArgs(GetAvatar(sessionId), vpObject));
        }

        private void OnQueryCellEndNative(IntPtr sender)
        {
            if (OnQueryCellEnd == null) return;
            int x;
            int z;
            lock (this)
            {
                x = Functions.vp_int(sender, IntegerAttribute.CellX);
                z = Functions.vp_int(sender, IntegerAttribute.CellZ);
            }
            OnQueryCellEnd(this, new QueryCellEndArgs(new Cell(x, z)));
        }

        private void OnWorldListNative(IntPtr sender)
        {
            if (OnWorldList == null)
                return;

            World data;
            lock (this)
            {
                string worldName = Functions.vp_string(InternalInstance, StringAttribute.WorldName);
                data = new World()
                {
                    Name = worldName,
                    State = (WorldState)Functions.vp_int(InternalInstance, IntegerAttribute.WorldState),
                    UserCount = Functions.vp_int(InternalInstance, IntegerAttribute.WorldUsers)
                };
            }
            if (_worlds.ContainsKey(data.Name))
                _worlds.Remove(data.Name);
            _worlds.Add(data.Name,data);
            OnWorldList(this, new WorldListEventArgs(data));
        }

        private void OnWorldSettingNativeEvent(IntPtr instance)
        {
            if (!_worlds.ContainsKey(Configuration.World.Name))
            {
                _worlds.Add(Configuration.World.Name,Configuration.World);
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

            OnWorldSettingsChanged?.Invoke(this, new WorldSettingsChangedEventArgs(_worlds[Configuration.World.Name]));
        }

        private void OnUniverseDisconnectNative(IntPtr sender)
        {
            if (OnUniverseDisconnect == null) return;
            OnUniverseDisconnect(this, new UniverseDisconnectEventArgs(Universe));
        }

        private void OnWorldDisconnectNative(IntPtr sender)
        {
            if (OnWorldDisconnect == null) return;
            OnWorldDisconnect(this, new WorldDisconnectEventArgs(World));
        }

        private void OnJoinNative(IntPtr sender)
        {
            if (OnJoin == null) return;

            lock (this)
            {
                int requestId = Functions.vp_int(sender, IntegerAttribute.JoinId);
                int userId = Functions.vp_int(sender, IntegerAttribute.UserId);
                string name = Functions.vp_string(sender, StringAttribute.JoinName);

                var request = new JoinRequest(this, requestId, userId, name);
                var args = new JoinEventArgs(request);
                OnJoin?.Invoke(this, args);
            }
        }

        #endregion

        #region Cleanup

        public void ReleaseEvents()
        {
            lock (this)
            {
                OnChatMessage = null;
                OnAvatarEnter = null;
                OnAvatarChange = null;
                OnAvatarLeave = null;
                OnObjectCreate = null;
                OnObjectChange = null;
                OnObjectDelete = null;
                OnObjectClick = null;
                OnObjectBump = null;
                OnWorldList = null;
                OnWorldDisconnect = null;
                OnWorldSettingsChanged = null;
                OnWorldDisconnect = null;
                OnUniverseDisconnect = null;
                OnUserAttributes = null;
                OnQueryCellResult = null;
                OnQueryCellEnd = null;
                OnFriendAddCallback = null;
                OnFriendsGetCallback = null;
            }
        }

        public void Dispose()
        {
            if (InternalInstance != IntPtr.Zero)
            {
                Functions.vp_destroy(InternalInstance);
            }
            
            if (_instanceHandle != GCHandle.FromIntPtr(IntPtr.Zero))
            {
                _instanceHandle.Free();
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Friend Functions

        public void GetFriends()
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friends_get(InternalInstance));
            }
        }

        public void AddFriendByName(Friend friend)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_add_by_name(InternalInstance, friend.Name));
            }
        }

        public void AddFriendByName(string name)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_add_by_name(InternalInstance, name));
            }
        }

        public void DeleteFriendById(int friendId)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_delete(InternalInstance, friendId));
            }
        }

        public void DeleteFriendById(Friend friend)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_friend_delete(InternalInstance, friend.UserId));
            }
        }

        #endregion

        #region ITerrainFunctions Implementation

        public void TerrianQuery(int tileX, int tileZ, int[,] nodes)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_terrain_query(InternalInstance, tileX, tileZ, nodes));
            }
        }

        public void SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells)
        {
            lock (this)
            {
                CheckReasonCode(Functions.vp_terrain_node_set(InternalInstance, tileX, tileZ, nodeX, nodeZ, cells));
            }
        }

        #endregion

        #region Implementation of IInstanceEvents
        
        #endregion


    }
}
