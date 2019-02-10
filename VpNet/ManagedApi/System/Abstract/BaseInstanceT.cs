#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2016 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using VpNet.Cache;
using VpNet.Extensions;
using VpNet.Interfaces;
using VpNet.ManagedApi.Extensions;
using VpNet.ManagedApi.System;
using VpNet.NativeApi;
using Attribute = VpNet.NativeApi.Attributes;

namespace VpNet.Abstract
{
    /// <summary>
    /// Abtract fully teamplated instance class, providing .NET encapsulation strict templated types to the native C wrapper.
    /// </summary>
    /// <typeparam name="T">Type of the abstract implementation</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTerrainCell">The type of the terrain cell.</typeparam>
    /// <typeparam name="TTerrainNode">The type of the terrain node.</typeparam>
    /// <typeparam name="TTerrainTile">The type of the terrain tile.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TWorldAttributes">The type of the world attributes.</typeparam>
    /// <typeparam name="TCell">The type of the cell.</typeparam>
    /// <typeparam name="TChatMessage">The type of the chat message.</typeparam>
    /// <typeparam name="TTerrain">The type of the terrain.</typeparam>
    /// <typeparam name="TUniverse">The type of the universe.</typeparam>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    [Serializable]
    public abstract partial class BaseInstanceT<T,
        /* Scene Type specifications ----------------------------------------------------------------------------------------------------------------------------------------------*/
        TAvatar, TColor, TFriend, TResult, TTerrainCell, TTerrainNode,
        TTerrainTile, TVector3, TVpObject, TWorld, TWorldAttributes,TCell,TChatMessage,TTerrain,TUniverse,TTeleport,
        TUserAttributes,THud
        > :
        /* Interface specifications -----------------------------------------------------------------------------------------------------------------------------------------*/
        /* Functions */
        BaseInstanceEvents<TWorld>,
        IAvatarFunctions<TResult, TAvatar, TVector3>,
        IChatFunctions<TResult, TAvatar, TColor, TVector3>,
        IFriendFunctions<TResult, TFriend>,
        ITeleportFunctions<TResult, TWorld, TAvatar, TVector3>,
        ITerrainFunctions<TResult, TTerrainTile, TTerrainNode, TTerrainCell>,
        IVpObjectFunctions<TResult, TVpObject, TVector3>,
        IWorldFunctions<TResult, TWorld, TWorldAttributes>,
        IUniverseFunctions<TResult>
/* Constraints ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        where TUniverse : class, IUniverse, new()
        where TTerrain : class, ITerrain, new()
        where TCell : class, ICell, new()
        where TChatMessage : class, IChatMessage<TColor>, new()
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode, TTerrainCell>, new()
        where TResult : class, IRc, new()
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar<TVector3>, new()
        where TFriend : class, IFriend, new()
        where TColor : class, IColor, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TVector3 : struct, IVector3
        where TWorldAttributes : class, IWorldAttributes, new()
        where TTeleport : class, ITeleport<TWorld,TAvatar,TVector3>, new()
        where TUserAttributes : class, IUserAttributes, new()
        where THud : IHud<TAvatar,TVector3>
        where T : class, new()
    {
        bool _isInitialized;

        public OpCacheProvider ModelCacheProvider { get; internal set; }
  
        private readonly Dictionary<int, TVpObject> _objectReferences = new Dictionary<int, TVpObject>();

        private Dictionary<int, TAvatar> _avatars;

        public T Implementor { get; set; }

        Dictionary<string, TWorld> _worlds;

        private static IHud<TAvatar,TVector3> _hud;

        public IHud<TAvatar, TVector3> Hud { get { return _hud; } set { _hud = value; } }

        private TUniverse Universe { get; set; }
        private TWorld World { get; set; }
        private NetConfig netConfig;
        private GCHandle instanceHandle;
        private TaskCompletionSource<TResult> ConnectCompletionSource;
        private TaskCompletionSource<TResult> LoginCompletionSource;
        private TaskCompletionSource<TResult> EnterCompletionSource;

        internal void Init()
        {
            Universe = new TUniverse();
            World = new TWorld();
            ((IAvatarFunctions<TResult, TAvatar, TVector3>) this).Avatars = new Dictionary<int, TAvatar>();
            _avatars =  ((IAvatarFunctions<TResult, TAvatar, TVector3>) this).Avatars;
            _worlds = new Dictionary<string, TWorld>();
            _isInitialized = true;
        }

        internal void InitOnce()
        {
            instanceHandle = GCHandle.Alloc(this);
            netConfig.Context = GCHandle.ToIntPtr(instanceHandle);
            netConfig.Create = Connection.CreateNative;
            netConfig.Destroy = Connection.DestroyNative;
            netConfig.Connect = Connection.ConnectNative;
            netConfig.Receive = Connection.ReceiveNative;
            netConfig.Send = Connection.SendNative;
            netConfig.Timeout = Connection.TimeoutNative;

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

            OnFriendAddCallbackNativeEvent += OnFriendAddCallbackNative;
            OnFriendDeleteCallbackNativeEvent += OnFriendDeleteCallbackNative;
            OnGetFriendsCallbackNativeEvent += OnGetFriendsCallbackNative;
        }

        private bool HasParentInstance { get;set; }

        internal protected BaseInstanceT(BaseInstanceEvents<TWorld> parentInstance)
        {
            HasParentInstance = true;
            _instance = parentInstance._instance;
            Init();
            _avatars = ((IAvatarFunctions<TResult, TAvatar, TVector3>) parentInstance).Avatars;
            Configuration = parentInstance.Configuration;
            Configuration.IsChildInstance = true;
            parentInstance.OnChatNativeEvent += OnChatNative;
            parentInstance.OnAvatarAddNativeEvent += OnAvatarAddNative;
            parentInstance.OnAvatarChangeNativeEvent += OnAvatarChangeNative;
            parentInstance.OnAvatarDeleteNativeEvent += OnAvatarDeleteNative;
            parentInstance.OnAvatarClickNativeEvent += OnAvatarClickNative;
            parentInstance.OnWorldListNativeEvent += OnWorldListNative;
            parentInstance.OnWorldDisconnectNativeEvent += OnWorldDisconnectNative;

            parentInstance.OnObjectChangeNativeEvent += OnObjectChangeNative;
            parentInstance.OnObjectCreateNativeEvent += OnObjectCreateNative;
            parentInstance.OnObjectClickNativeEvent += OnObjectClickNative;
            parentInstance.OnObjectBumpNativeEvent += OnObjectBumpNative;
            parentInstance.OnObjectBumpEndNativeEvent += OnObjectBumpEndNative; 
            parentInstance.OnObjectDeleteNativeEvent += OnObjectDeleteNative;
            parentInstance.OnQueryCellEndNativeEvent += OnQueryCellEndNative;
            parentInstance.OnUniverseDisconnectNativeEvent += OnUniverseDisconnectNative;
            parentInstance.OnTeleportNativeEvent += OnTeleportNative;
            parentInstance.OnUserAttributesNativeEvent += OnUserAttributesNative;
            parentInstance.OnJoinNativeEvent += OnJoinNative;

            parentInstance.OnObjectCreateCallbackNativeEvent += OnObjectCreateCallbackNative;
            parentInstance.OnObjectChangeCallbackNativeEvent += OnObjectChangeCallbackNative;
            parentInstance.OnObjectDeleteCallbackNativeEvent += OnObjectDeleteCallbackNative;
            parentInstance.OnObjectGetCallbackNativeEvent += OnObjectGetCallbackNative;

            parentInstance.OnFriendAddCallbackNativeEvent += OnFriendAddCallbackNative;
            parentInstance.OnFriendDeleteCallbackNativeEvent += OnFriendDeleteCallbackNative;
            parentInstance.OnGetFriendsCallbackNativeEvent += OnGetFriendsCallbackNative;
        }

        protected BaseInstanceT(InstanceConfiguration<TWorld> configuration)
        {
            InitOnce();
            InitVpNative();
            // this can't be a child instance.
            configuration.IsChildInstance = false;
            Configuration = configuration;
        }

        private void InitVpNative()
        {
            if (!_isInitialized)
            {
                Init();
                Configuration = new InstanceConfiguration<TWorld>(false);
                int rc = Functions.vp_init(4);
                if (rc != 0)
                {
                    if (rc != 3)
                        throw new VpException((ReasonCode)rc);
                    //vp previously initialized. do nothing.
                }

            }
            _instance = Functions.vp_create(ref netConfig);

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
            SetNativeCallback(Callbacks.FriendAdd, OnFriendAddCallbackNative1);
            SetNativeCallback(Callbacks.FriendDelete, OnFriendDeleteCallbackNative1);
            SetNativeCallback(Callbacks.GetFriends, OnGetFriendsCallbackNative1);
            //SetNativeCallback(Callbacks.ObjectLoad, OnObjectLoadCallbackNative1);
            SetNativeCallback(Callbacks.Login, OnLoginCallbackNative1);
            SetNativeCallback(Callbacks.Enter, OnEnterCallbackNativeEvent1);
            //SetNativeCallback(Callbacks.Join, OnJoinCallbackNativeEvent1);
            SetNativeCallback(Callbacks.ConnectUniverse, OnConnectUniverseCallbackNative1);
            //SetNativeCallback(Callbacks.WorldPermissionUserSet, OnWorldPermissionUserSetCallbackNative1);
            //SetNativeCallback(Callbacks.WorldPermissionSessionSet, OnWorldPermissionSessionSetCallbackNative1);
            //SetNativeCallback(Callbacks.WorldSettingSet, OnWorldSettingsSetCallbackNative1);
        }

        protected BaseInstanceT()
        {
            InitOnce();
            InitVpNative();
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
                SetCompletionResultFromRc(LoginCompletionSource, rc);
            }
        }
        internal void OnEnterCallbackNativeEvent1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResultFromRc(EnterCompletionSource, rc);
            }
        }
        internal void OnJoinCallbackNativeEvent1(IntPtr instance, int rc, int reference) { lock (this) { OnJoinCallbackNativeEvent(instance, rc, reference); } }
        internal void OnConnectUniverseCallbackNative1(IntPtr instance, int rc, int reference)
        {
            lock (this)
            {
                SetCompletionResultFromRc(ConnectCompletionSource, rc);
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

        private void SetCompletionResultFromRc(TaskCompletionSource<TResult> cs, int rc)
        {
            try
            {
                var result = new TResult { Rc = rc };
                cs.SetResult(result);
            } catch (Exception e)
            {
                cs.SetException(e);
            }
        }

        #region IUniverseFunctions Implementations

        virtual public Task<TResult> ConnectAsync(string host = "universe.virtualparadise.org", ushort port = 57000)
        {
            Universe.Host = host;
            Universe.Port = port;

            lock (this)
            {
                ConnectCompletionSource = new TaskCompletionSource<TResult>();
                var rc = Functions.vp_connect_universe(_instance, host, port);
                if (rc != 0)
                {
                    SetCompletionResultFromRc(ConnectCompletionSource, rc);
                }
                return ConnectCompletionSource.Task;
            }
        }

        virtual public async Task<TResult> LoginAndEnterAsync(bool announceAvatar = true)
        {
            await ConnectAsync();
            await LoginAsync();
            if (announceAvatar)
            {
                await EnterAsync();
                return UpdateAvatar();
            }
            return await EnterAsync();
        }

        virtual public async Task<TResult> LoginAsync()
        {
            if (Configuration == null ||
                string.IsNullOrEmpty(Configuration.BotName) ||
                string.IsNullOrEmpty(Configuration.Password) ||
                string.IsNullOrEmpty(Configuration.UserName)
                )
            {
                throw new ArgumentException("Can't login because of Incomplete login configuration.");
            }
            return await LoginAsync(Configuration.UserName, Configuration.Password, Configuration.BotName);
        }

        virtual public Task<TResult> LoginAsync(string username, string password, string botname)
        {
            lock (this)
            {
                Configuration.BotName = botname;
                Configuration.UserName = username;
                Configuration.Password = password;

                LoginCompletionSource = new TaskCompletionSource<TResult>();
                var rc = Functions.vp_login(_instance, username, password, botname);
                if (rc != 0)
                {
                    return Task.FromResult(new TResult { Rc = rc });
                }

                return LoginCompletionSource.Task;
            }
        }

        #endregion

        #region IWorldFunctions Implementations
        [Obsolete("No longer necessary for network IO to occur")]
        virtual public TResult Wait(int milliseconds = 10)
        {
            Thread.Sleep(milliseconds);
            return new TResult { Rc = 0 };
        }

        virtual public Task<TResult> EnterAsync(string worldname)
        {
            return EnterAsync(new TWorld { Name = worldname });
        }

        virtual public Task<TResult> EnterAsync()
        {
            if (Configuration == null || Configuration.World == null || string.IsNullOrEmpty(Configuration.World.Name))
                throw new ArgumentException("Can't login because of Incomplete instance world configuration.");
            return EnterAsync(Configuration.World);
        }

        virtual public Task<TResult> EnterAsync(TWorld world)
        {
            lock (this)
            {
                Configuration.World = world;

                EnterCompletionSource = new TaskCompletionSource<TResult>();
                var rc = Functions.vp_enter(_instance, world.Name);
                if (rc != 0)
                {
                    return Task.FromResult(new TResult { Rc = rc });
                }

                return EnterCompletionSource.Task;
            }
        }

        virtual public TAvatar My()
        {
            return new TAvatar
            {
                UserId = Functions.vp_int(_instance, Attribute.MyUserId),
                Name = Configuration.BotName,
                AvatarType = Functions.vp_int(_instance, Attribute.MyType),
                Position = new TVector3
                {
                    X = Functions.vp_double(_instance, Attribute.MyX).Truncate(3),
                    Y = Functions.vp_double(_instance, Attribute.MyY).Truncate(3),
                    Z = Functions.vp_double(_instance, Attribute.MyZ).Truncate(3)
                },
                Rotation = new TVector3
                {
                    X = Functions.vp_double(_instance, Attribute.MyPitch).Truncate(3),
                    Y = Functions.vp_double(_instance, Attribute.MyYaw).Truncate(3),
                    Z = 0 /* roll currently not supported*/
                },
                LastChanged = DateTime.Now
            };
        }

        /// <summary>
        /// Leave the current world
        /// </summary>
        virtual public TResult Leave()
        {
            lock (this)
            {
                var result = new TResult {Rc = Functions.vp_leave(_instance)};
                if (result.Rc == 0 && OnWorldLeave !=null)
                {
                    OnWorldLeave(Implementor,new WorldLeaveEventArgsT<TWorld> {World = Configuration.World});
                }
                return result;
            }
        }

        virtual public void Disconnect()
        {
            _avatars.Clear();
            Functions.vp_destroy(_instance);
            _isInitialized = false;
            InitVpNative();
            if (OnUniverseDisconnect != null)
                OnUniverseDisconnect(Implementor, new UniverseDisconnectEventArgsT<TUniverse> { Universe = Universe,DisconnectType = VpNet.DisconnectType.UserDisconnected  });
        }

        virtual public TResult ListWorlds()
        {
            lock (this)
            {
                return new TResult {Rc = Functions.vp_world_list(_instance, 0)};
            }
        }

        #endregion

        #region IQueryCellFunctions Implementation

        virtual public TResult QueryCell(int cellX, int cellZ)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_query_cell(_instance, cellX, cellZ) };
            }
        }

        virtual public TResult QueryCell(int cellX, int cellZ, int revision)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_query_cell_revision(_instance, cellX, cellZ,revision) };
            }
        }

        #endregion

        #region IVpObjectFunctions implementations

        public TResult ClickObject(TVpObject vpObject)
        {
            lock (this)
            {
                return ClickObject(vpObject.Id);
            }
        }

        public TResult ClickObject(int objectId)
        {
            lock (this)
            {
               // Functions.vp_int_set(_instance, Attributes.ObjectId,objectId);
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance, objectId,0,0,0,0)
                };
            }
        }

        public TResult ClickObject(TVpObject vpObject, TAvatar avatar)
        {
            lock (this)
            {
                return ClickObject(vpObject.Id, avatar.Session);
            }
        }

        public TResult ClickObject(TVpObject vpObject, TAvatar avatar, TVector3 worldHit)
        {
            lock (this)
            {
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance, vpObject.Id, avatar.Session, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z)
                };
            }
        }

        public TResult ClickObject(TVpObject vpObject, TVector3 worldHit)
        {
            lock (this)
            {
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance, vpObject.Id, 0, (float)worldHit.X, (float)worldHit.Y, (float)worldHit.Z)
                };
            }
        }

        public TResult ClickObject(int objectId,int toSession, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance, objectId, toSession, (float)worldHitX, (float)worldHitY, (float)worldHitZ)
                };
            }
        }

        public TResult ClickObject(int objectId, double worldHitX, double worldHitY, double worldHitZ)
        {
            lock (this)
            {
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance, objectId, 0, (float)worldHitX, (float)worldHitY, (float)worldHitZ)
                };
            }
        }


        public TResult ClickObject(int objectId, int toSession)
        {
            lock (this)
            {
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance,objectId,toSession,0,0,0)
                };
            }
        }

        virtual public TResult DeleteObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            lock (this)
            {
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                rc = Functions.vp_object_delete(_instance,vpObject.Id);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult LoadObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            lock (this)
            {
                vpObject.ReferenceNumber = referenceNumber; // calculated a unqiue id for you.
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(_instance, Attribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(_instance, Attribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(_instance, Attribute.ObjectModel, vpObject.Model);
                Functions.SetData(_instance, Attribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(_instance, Attribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(_instance, Attribute.ObjectType, vpObject.ObjectType);
                Functions.vp_int_set(_instance, Attribute.ObjectUserId, vpObject.Owner);
                Functions.vp_int_set(_instance, Attribute.ObjectTime, (vpObject.Time - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Seconds);
                rc = Functions.vp_object_load(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult AddObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            lock (this)
            {
                vpObject.ReferenceNumber = referenceNumber; // calculated a unqiue id for you.
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(_instance, Attribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(_instance, Attribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(_instance, Attribute.ObjectModel, vpObject.Model);
                Functions.SetData(_instance, Attribute.ObjectData, vpObject.Data);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(_instance, Attribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(_instance, Attribute.ObjectType, vpObject.ObjectType);
                rc = Functions.vp_object_add(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult ChangeObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = ObjectReferenceCounter.GetNextReference();
            lock (this)
            {
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(_instance, Attribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(_instance, Attribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(_instance, Attribute.ObjectModel, vpObject.Model);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectX, vpObject.Position.X);
                Functions.vp_double_set(_instance, Attribute.ObjectY, vpObject.Position.Y);
                Functions.vp_double_set(_instance, Attribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_double_set(_instance, Attribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(_instance, Attribute.ObjectType, vpObject.ObjectType);
                rc = Functions.vp_object_change(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult GetObject(int id)
        {
            lock (this)
            {
                return new TResult {Rc= Functions.vp_object_get(_instance, id)};
            }
        }

        #endregion

        #region ITeleportFunctions Implementations

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, double x, double y, double z, double yaw, double pitch)
        {
            return new TResult
                {
                    Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, (float)x, (float)y, (float)z, (float)yaw, (float)pitch)
                };
        }

        virtual public TResult TeleportAvatar(int targetSession, string world, double x, double y, double z, double yaw, double pitch)
        {
            return new TResult
                {
                    Rc = Functions.vp_teleport_avatar(_instance, targetSession, world, (float)x, (float)y, (float)z, (float)yaw, (float)pitch)
                };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, TVector3 position, double yaw, double pitch)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, (float)position.X, (float)position.Y, (float)position.Z, (float)yaw, (float)pitch)
            };
        }

        virtual public TResult TeleportAvatar(int targetSession, string world, TVector3 position, double yaw, double pitch)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, targetSession, world, (float)position.X, (float)position.Y, (float)position.Z, (float)yaw, (float)pitch)
            };

        }

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, (float)position.X, (float)position.Y, (float)position.Z, (float)rotation.Y, (float)rotation.X)
            };

        }

        public TResult TeleportAvatar(TAvatar avatar, TWorld world, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world.Name, (float)position.X, (float)position.Y, (float)position.Z, (float)rotation.Y, (float)rotation.X)
            };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, string.Empty, (float)position.X, (float)position.Y, (float)position.Z, (float)rotation.Y, (float)rotation.X)
            };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, string.Empty, (float)avatar.Position.X, (float)avatar.Position.Y, (float)avatar.Position.Z, (float)avatar.Rotation.Y, (float)avatar.Rotation.X)
            };
        }

        #endregion

        #region IAvatarFunctions Implementations.

        virtual public TResult GetUserProfile(int userId)
        {
            return new TResult
            {
                Rc = Functions.vp_user_attributes_by_id(_instance, userId)
            };
        }

        [Obsolete]
        virtual public TResult GetUserProfile(string userName)
        {
            return new TResult
            {
                Rc = Functions.vp_user_attributes_by_name(_instance,userName)
            };
        }

        virtual public TResult GetUserProfile(TAvatar profile)
        {
            return GetUserProfile(profile.UserId);
        }

        virtual public TResult UpdateAvatar(double x = 0.0f, double y = 0.0f, double z = 0.0f,double yaw = 0.0f, double pitch = 0.0f)
        {
            lock (this)
            {
                Functions.vp_double_set(_instance, Attribute.MyX, x);
                Functions.vp_double_set(_instance, Attribute.MyY, y);
                Functions.vp_double_set(_instance, Attribute.MyZ, z);
                Functions.vp_double_set(_instance, Attribute.MyYaw, yaw);
                Functions.vp_double_set(_instance, Attribute.MyPitch, pitch);
                return new TResult
                {
                    Rc = Functions.vp_state_change(_instance)
                };

            }
        }

        public TResult UpdateAvatar(TVector3 position)
        {
            return UpdateAvatar(position.X, position.Y, position.Z);
        }

        public TResult UpdateAvatar(TVector3 position, TVector3 rotation)
        {
            return UpdateAvatar(position.X, position.Y, position.Z,rotation.X,rotation.Y);
        }

        public TResult AvatarClick(int session)
        {
            return new TResult {Rc = Functions.vp_avatar_click(_instance,session)};
        }

        public TResult AvatarClick(TAvatar avatar)
        {
            return new TResult { Rc = Functions.vp_avatar_click(_instance, avatar.Session) };
        }

        #endregion

        #region IChatFunctions Implementations

        virtual public TResult Say(string message)
        {
            lock (this)
            {
                return new TResult {Rc = Functions.vp_say(_instance, message)};
            }
        }

        virtual public TResult Say(string format, params object[] arg)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_say(_instance, string.Format(format,arg)) };
            }
        }

        public TResult ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = (TextEffectTypes) 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, targetSession, name, message, (int)effects, red, green, blue) };
        }

        public TResult ConsoleMessage(TAvatar avatar, string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, avatar.Session, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(int targetSession, string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, targetSession, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, string.Empty, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string message)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, string.Empty, message, 0, 0, 0, 0) };
        }

        virtual public TResult ConsoleMessage(TAvatar avatar, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, avatar.Session, name, message, (int)effects, red, green, blue) };
        }

        virtual public TResult UrlSendOverlay(TAvatar avatar, string url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatar.Session, url, (int)Attribute.UrlTargetOverlay) };
        }

        virtual public TResult UrlSendOverlay(TAvatar avatar, Uri url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatar.Session, url.AbsoluteUri, (int)Attribute.UrlTargetOverlay) };
        }

        virtual public TResult UrlSendOverlay(int avatarSession, string url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatarSession, url, (int)Attribute.UrlTargetOverlay) };
        }

        virtual public TResult UrlSendOverlay(int avatarSession, Uri url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatarSession, url.AbsoluteUri, (int)Attribute.UrlTargetOverlay) };
        }

        virtual public TResult UrlSend(TAvatar avatar, string url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatar.Session, url, (int)Attribute.UrlTargetBrowser) };
        }

        virtual public TResult UrlSend(TAvatar avatar, Uri url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatar.Session, url.AbsoluteUri, (int)Attribute.UrlTargetBrowser) };
        }

        virtual public TResult UrlSend(int avatarSession, string url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatarSession, url, (int)Attribute.UrlTargetBrowser) };
        }

        virtual public TResult UrlSend(int avatarSession, Uri url)
        {
            return new TResult { Rc = Functions.vp_url_send(_instance, avatarSession, url.AbsoluteUri, (int)Attribute.UrlTargetBrowser) };
        }

        #endregion

        #region IJoinFunctions Implementations
        public virtual TResult Join(TAvatar avatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_join(_instance, avatar.UserId) };
            }
        }

        public virtual TResult Join(int userId)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_join(_instance, userId) };
            }
        }


        public virtual TResult JoinAccept(int requestId, string world, TVector3 location, float yaw, float pitch)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_join_accept(_instance, requestId, world,location.X,location.Y,location.Z,yaw,pitch) };
            }
        }

        public virtual TResult JoinAccept(int requestId, string world, double x, double y, double z, float yaw, float pitch)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_join_accept(_instance, requestId, world, x, y, z, yaw, pitch) };
            }
        }

        public virtual TResult JoinDecline(int requestId)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_join_decline(_instance, requestId) };
            }
        }

        #endregion

        #region  IWorldPermissionFunctions Implementations

        public virtual TResult WorldPermissionUser(string permission, int userId, int enable)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, permission, userId, enable) };
            }
        }

        public virtual TResult WorldPermissionUserEnable(WorldPermissions permission, TAvatar avatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission),avatar.UserId,1) };
            }
        }

        public virtual TResult WorldPermissionUserEnable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), userId, 1) };
            }
        }

        public virtual TResult WorldPermissionUserDisable(WorldPermissions permission, TAvatar avatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), avatar.UserId, 0) };
            }
        }

        public virtual TResult WorldPermissionUserDisable(WorldPermissions permission, int userId)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), userId, 0) };
            }
        }

        public virtual TResult WorldPermissionSession(string permission, int sessionId, int enable)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_session_set(_instance, permission, sessionId, enable) };
            }
        }

        public virtual TResult WorldPermissionSessionEnable(WorldPermissions permission, TAvatar avatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 1) };
            }
        }

        public virtual TResult WorldPermissionSessionEnable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), session, 1) };
            }
        }


        public virtual TResult WorldPermissionSessionDisable(WorldPermissions permission, TAvatar avatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), avatar.Session, 0) };
            }
        }

        public virtual TResult WorldPermissionSessionDisable(WorldPermissions permission, int session)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_permission_user_set(_instance, Enum.GetName(typeof(WorldPermissions), permission), session, 0) };
            }
        }

        #endregion

        #region IWorldSettingsFunctions Implementations

        public virtual TResult WorldSettingSession(string setting, string value, TAvatar toAvatar)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_setting_set(_instance, setting, value, toAvatar.Session) };
            }
        }

        public virtual TResult WorldSettingSession(string setting, string value, int  toSession)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_world_setting_set(_instance, setting, value, toSession) };
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
            Functions.vp_event_set(_instance, (int)eventType, eventFunction);
        }

        private void SetNativeCallback(Callbacks callbackType, CallbackDelegate callbackFunction)
        {
            _nativeCallbacks[callbackType] = callbackFunction;
            Functions.vp_callback_set(_instance, (int)callbackType, callbackFunction);
        }

        //public delegate void Event(T sender);
        public delegate void ChatMessageDelegate(T sender, ChatMessageEventArgsT<TAvatar, TChatMessage, TVector3, TColor> args);

        public delegate void AvatarChangeDelegate(T sender, AvatarChangeEventArgsT<TAvatar,TVector3> args);
        public delegate void AvatarEnterDelegate(T sender, AvatarEnterEventArgsT<TAvatar, TVector3> args);
        public delegate void AvatarLeaveDelegate(T sender, AvatarLeaveEventArgsT<TAvatar, TVector3> args);
        public delegate void AvatarClickDelegate(T sender, AvatarClickEventArgsT<TAvatar, TVector3> args);

        public delegate void TeleportDelegate(T sender, TeleportEventArgsT<TTeleport, TWorld, TAvatar, TVector3> args);

        public delegate void UserAttributesDelegate(T sender, UserAttributesEventArgsT<TUserAttributes> args);

        public delegate void WorldListEventDelegate(T sender, WorldListEventArgsT<TWorld> args);

        public delegate void ObjectCreateDelegate(T sender, ObjectCreateArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void ObjectChangeDelegate(T sender, ObjectChangeArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void ObjectDeleteDelegate(T sender, ObjectDeleteArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void ObjectClickDelegate(T sender, ObjectClickArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void ObjectBumpDelegate(T sender, ObjectBumpArgsT<TAvatar, TVpObject, TVector3> args);


        public delegate void ObjectCreateCallback(T sender, ObjectCreateCallbackArgsT<TResult, TVpObject, TVector3> args);
        public delegate void ObjectChangeCallback(T sender, ObjectChangeCallbackArgsT<TResult, TVpObject, TVector3> args);
        public delegate void ObjectDeleteCallback(T sender, ObjectDeleteCallbackArgsT<TResult, TVpObject, TVector3> args);
        public delegate void ObjectGetCallback(T sender, ObjectGetCallbackArgsT<TResult, TVpObject, TVector3> args);

        public delegate void QueryCellResultDelegate(T sender, QueryCellResultArgsT<TVpObject, TVector3> args);
        public delegate void QueryCellEndDelegate(T sender, QueryCellEndArgsT<TCell> args);

        public delegate void WorldSettingsChangedDelegate(T sender, WorldSettingsChangedEventArgsT<TWorld> args);
        public delegate void WorldDisconnectDelegate(T sender, WorldDisconnectEventArgsT<TWorld> args);

        public delegate void UniverseDisconnectDelegate(T sender, UniverseDisconnectEventArgsT<TUniverse> args);
        public delegate void JoinDelegate(T sender, JoinEventArgsT args);

        public delegate void FriendAddCallbackDelegate(T sender, FriendAddCallbackEventArgsT<TFriend> args);
        public delegate void FriendDeleteCallbackDelegate(T sender, FriendDeleteCallbackEventArgsT<TFriend> args);
        public delegate void FriendsGetCallbackDelegate(T sender, FriendsGetCallbackEventArgsT<TFriend> args);

        public event ChatMessageDelegate OnChatMessage;
        public event AvatarEnterDelegate OnAvatarEnter;
        public event AvatarChangeDelegate OnAvatarChange;
        public event AvatarLeaveDelegate OnAvatarLeave;
        public event AvatarClickDelegate OnAvatarClick;
        public event JoinDelegate OnJoin;

        public event TeleportDelegate OnTeleport;
        public event UserAttributesDelegate OnUserAttributes;

        public event ObjectCreateDelegate OnObjectCreate;
        public event ObjectChangeDelegate OnObjectChange;
        public event ObjectDeleteDelegate OnObjectDelete;
        public event ObjectClickDelegate OnObjectClick;
        public event ObjectBumpDelegate OnObjectBump;

        public event ObjectCreateCallback OnObjectCreateCallback;
        public event ObjectDeleteCallback OnObjectDeleteCallback;
        public event ObjectChangeCallback OnObjectChangeCallback;
        public event ObjectGetCallback OnObjectGetCallback; 


        public event WorldListEventDelegate OnWorldList;
        public event WorldSettingsChangedDelegate OnWorldSettingsChanged;
        public event FriendAddCallbackDelegate OnFriendAddCallback;
        public event FriendDeleteCallbackDelegate OnFriendDeleteCallback;
        public event FriendsGetCallbackDelegate OnFriendsGetCallback;

        public event WorldDisconnectDelegate OnWorldDisconnect;
        public event UniverseDisconnectDelegate OnUniverseDisconnect;

        public event QueryCellResultDelegate OnQueryCellResult;
        public event QueryCellEndDelegate OnQueryCellEnd;

        /* Events indirectly assosicated with VP */

        public delegate void WorldEnterDelegate(T sender, WorldEnterEventArgsT<TWorld> args);
        public event WorldEnterDelegate OnWorldEnter;
        public delegate void WorldLeaveDelegate(T sender, WorldLeaveEventArgsT<TWorld> args);
        public event WorldLeaveDelegate OnWorldLeave;

        #endregion

        #region CallbackHandlers

        private void OnObjectCreateCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);
                if (OnObjectCreateCallback != null)
                {
                    vpObject.Id = Functions.vp_int(sender, Attribute.ObjectId);
                    OnObjectCreateCallback(Implementor, new ObjectCreateCallbackArgsT<TResult, TVpObject, TVector3> { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        private void OnObjectChangeCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);

                if (OnObjectChangeCallback != null)
                {
                    vpObject.Id = Functions.vp_int(sender, Attribute.ObjectId);
                    OnObjectChangeCallback(Implementor, new ObjectChangeCallbackArgsT<TResult, TVpObject, TVector3> { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        private void OnObjectDeleteCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);

                if (OnObjectDeleteCallback != null)
                {
                    OnObjectDeleteCallback(Implementor, new ObjectDeleteCallbackArgsT<TResult, TVpObject, TVector3> { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        void OnObjectGetCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                TVpObject vpObject;
                GetVpObject(sender,out vpObject);
                if (OnObjectGetCallback != null)
                {
                    OnObjectGetCallback(Implementor, new ObjectGetCallbackArgsT<TResult, TVpObject, TVector3> { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        private void OnObjectLoadCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */ }
        private void OnLoginCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }
        private void OnEnterCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }
        private void OnJoinCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }
        private void OnConnectUniverseCallbackNative(IntPtr sender, int rc, int reference) {
            SetCompletionResultFromRc(ConnectCompletionSource, rc);
        }
        private void OnWorldPermissionUserSetCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }
        private void OnWorldPermissionSessionSetCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }
        private void OnWorldSettingsSetCallbackNative(IntPtr sender, int rc, int reference) { /* todo: implement this */  }

        #endregion

        #region Event handlers

        private void OnUserAttributesNative(IntPtr sender)
        {
            if (OnUserAttributes == null)
                return;
            TUserAttributes att;
            lock (this)
            {
                att = new TUserAttributes()
                          {
                              Email = Functions.vp_string(sender, Attributes.UserEmail),
                              Id = Functions.vp_int(sender, Attributes.UserId),
                              LastLogin =
                                  new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Functions.vp_int(sender,
                                                                                                Attribute.UserLastLogin)),
                              Name = Functions.vp_string(sender, Attributes.UserName),
                              OnlineTime = new TimeSpan(0, 0, 0, Functions.vp_int(sender, Attribute.UserOnlineTime)),
                              RegistrationDate =
                                  new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Functions.vp_int(sender,
                                                                                                Attribute.UserRegistrationTime))
                          };
            }
            OnUserAttributes(Implementor,new UserAttributesEventArgsT<TUserAttributes>(){UserAttributes = att});
        }

        private void OnTeleportNative(IntPtr sender)
        {
            if (OnTeleport == null)
                return;
            TTeleport teleport;
            lock (this)
            {
                teleport = new TTeleport
                    {
                        Avatar = GetAvatar(Functions.vp_int(sender, Attribute.AvatarSession)),
                        Position = new TVector3
                            {
                                X = Functions.vp_double(sender, Attribute.TeleportX),
                                Y = Functions.vp_double(sender, Attribute.TeleportY),
                                Z = Functions.vp_double(sender, Attribute.TeleportZ)
                            },
                        Rotation = new TVector3
                            {
                                X = Functions.vp_double(sender, Attribute.TeleportPitch),
                                Y = Functions.vp_double(sender, Attribute.TeleportYaw),
                                Z = 0 /* Roll not implemented yet */
                            },
                            // TODO: maintain user count and world state statistics.
                        World = new TWorld { Name = Functions.vp_string(sender, Attribute.TeleportWorld),State = WorldState.Unknown, UserCount=-1 }
                    };
            }
            OnTeleport(Implementor, new TeleportEventArgsT<TTeleport, TWorld, TAvatar, TVector3>{Teleport = teleport});
        }

        private void OnGetFriendsCallbackNative(IntPtr sender, int rc, int reference)
        {
            if (OnFriendAddCallback == null)
                return;
            lock (this)
            {
                var friend = new TFriend
                    {
                        UserId = Functions.vp_int(sender,Attributes.UserId),
                        Id = Functions.vp_int(sender,Attributes.FriendId),
                        Name = Functions.vp_string(sender,Attributes.FriendName),
                       Online = Functions.vp_int(sender,Attributes.FriendOnline)==1
                    };
                OnFriendsGetCallback(Implementor, new FriendsGetCallbackEventArgsT<TFriend> { Friend = friend });
            }
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
            ChatMessageEventArgsT<TAvatar, TChatMessage, TVector3, TColor> data;
            lock (this)
            {
                if (!_avatars.ContainsKey(Functions.vp_int(sender, Attribute.AvatarSession)))
                {
                    var avatar = new TAvatar
                        {
                            Name = Functions.vp_string(sender, Attribute.AvatarName),
                            Session = Functions.vp_int(sender, Attribute.AvatarSession)
                        };
                    _avatars.Add(avatar.Session, avatar);
                }
                data = new ChatMessageEventArgsT<TAvatar, TChatMessage, TVector3, TColor>
                {
                    Avatar = _avatars[Functions.vp_int(sender, Attribute.AvatarSession)],
                    ChatMessage = new TChatMessage
                    {
                        Type = (ChatMessageTypes)Functions.vp_int(sender, Attribute.ChatType),
                        Message = Functions.vp_string(sender, Attribute.ChatMessage),
                        Name = Functions.vp_string(sender, Attribute.AvatarName),
                        TextEffectTypes = (TextEffectTypes)Functions.vp_int(sender, Attributes.ChatEffects)
                    }
                };
                OperatingSystem os = Environment.OSVersion;

                if (data.ChatMessage.Message == string.Format("{0} version", Configuration.BotName) && !HasParentInstance )
                    ConsoleMessage(data.Avatar, "VPNET",
                        string.Format("Version {0} running on {1}", Assembly.GetAssembly(typeof (RcDefault)).GetName().Version,os.VersionString),TextEffectTypes.Bold,0,0,127);
                if (OnChatMessage == null) return;
                if (data.ChatMessage.Type == ChatMessageTypes.Console)
                {
                    data.ChatMessage.Color = new TColor
                                                 {
                                                     R = (byte) Functions.vp_int(sender, Attribute.ChatRolorRed),
                                                     G = (byte) Functions.vp_int(sender, Attribute.ChatColorGreen),
                                                     B = (byte) Functions.vp_int(sender, Attribute.ChatColorBlue)
                                                 };
                }
                else
                {
                    data.ChatMessage.Color = new TColor
                    {
                        R = 0,
                        G = 0,
                        B = 0
                    };
                }
            }
            OnChatMessage(Implementor, data);
        }

        private void OnAvatarAddNative(IntPtr sender)
        {
            TAvatar data;
            lock (this)
            {
                data = new TAvatar {UserId=Functions.vp_int(sender, Attribute.UserId),
                Name = Functions.vp_string(sender, Attribute.AvatarName),
                Session=Functions.vp_int(sender, Attribute.AvatarSession),
                AvatarType=Functions.vp_int(sender, Attribute.AvatarType),
                Position=new TVector3{X=Functions.vp_double(sender, Attribute.AvatarX).Truncate(3),
                            Y=Functions.vp_double(sender, Attribute.AvatarY).Truncate(3),
                            Z=Functions.vp_double(sender, Attribute.AvatarZ).Truncate(3)},
                Rotation=new TVector3{X=Functions.vp_double(sender, Attribute.AvatarPitch).Truncate(3),
                            Y=Functions.vp_double(sender, Attribute.AvatarYaw).Truncate(3),
                            Z=0 /* roll currently not supported*/}};
                if (!_avatars.ContainsKey(data.Session))
                    _avatars.Add(data.Session, data);
            }
            data.LastChanged = DateTime.UtcNow;
            if (OnAvatarEnter == null) return;
            var args = new AvatarEnterEventArgsT<TAvatar, TVector3> {Avatar = data, Implementor = Implementor};
            args.Initialize();
            OnAvatarEnter(Implementor, args);
        }

        private void OnAvatarChangeNative(IntPtr sender)
        {
            TAvatar old;
            TAvatar data;
            lock (this)
            {
                data = new TAvatar{UserId=_avatars[Functions.vp_int(sender, Attribute.AvatarSession)].UserId, Name=Functions.vp_string(sender, Attribute.AvatarName),
                                  Session=Functions.vp_int(sender, Attribute.AvatarSession),
                                  AvatarType=Functions.vp_int(sender, Attribute.AvatarType),
                                  Position=new TVector3{X=Functions.vp_double(sender, Attribute.AvatarX).Truncate(3),
                                  Y=Functions.vp_double(sender, Attribute.AvatarY).Truncate(3),
                                  Z=Functions.vp_double(sender, Attribute.AvatarZ).Truncate(3)},
               Rotation=new TVector3{X=Functions.vp_double(sender, Attribute.AvatarPitch).Truncate(3),
                            Y=Functions.vp_double(sender, Attribute.AvatarYaw).Truncate(3),
                            Z=0 /* roll currently not supported*/}};
                // determine if the avatar actually changed.
                old = new TAvatar().CopyFrom(_avatars[data.Session], true);
                if (data.Position.X == old.Position.X
                    && data.Position.Y == old.Position.Y
                    && data.Position.Z == old.Position.Z
                    && data.Rotation.X == old.Rotation.X
                    && data.Rotation.Y == old.Rotation.Y
                    && data.Rotation.Z == old.Rotation.Z)
                    return;
                data.LastChanged = DateTime.UtcNow;
                setAvatar(data);

            }
            if (OnAvatarChange != null)
                OnAvatarChange(Implementor, new AvatarChangeEventArgsT<TAvatar, TVector3> { Avatar = _avatars[data.Session], AvatarPrevious = old });
        }

        private void OnAvatarDeleteNative(IntPtr sender)
        {
            TAvatar data;
            lock (this)
            {
                try
                {
                    data = _avatars[Functions.vp_int(sender, Attribute.AvatarSession)];
                    _avatars.Remove(data.Session);
                    if (OnAvatarLeave == null) return;
                    OnAvatarLeave(Implementor, new AvatarLeaveEventArgsT<TAvatar, TVector3> { Avatar = data });
                }
                catch
                {
                    
                }
            }
        }

        private void OnAvatarClickNative(IntPtr sender)
        {
            if (OnAvatarClick == null) return;
            lock (this)
            {
                var clickedAvatar = Functions.vp_int(sender, Attribute.ClickedSession);
                if (clickedAvatar == 0)
                    clickedAvatar = Functions.vp_int(sender, Attribute.AvatarSession);

                OnAvatarClick(Implementor,
                    new AvatarClickEventArgsT<TAvatar, TVector3>
                    {
                        Avatar = GetAvatar(Functions.vp_int(sender, Attribute.AvatarSession)),
                        ClickedAvatar = GetAvatar(clickedAvatar),
                        WorldHit = new TVector3
                        {
                            X = Functions.vp_double(sender, Attribute.ClickHitX),
                            Y = Functions.vp_double(sender, Attribute.ClickHitY),
                            Z = Functions.vp_double(sender, Attribute.ClickHitZ)
                        }
                    });
            }
        }

        private void OnObjectClickNative(IntPtr sender)
        {
            if (OnObjectClick == null) return;
            int session;
            int objectId;
            TVector3 world;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
                world = new TVector3
                    {
                        X = Functions.vp_double(sender, Attribute.ClickHitX),
                        Y = Functions.vp_double(sender, Attribute.ClickHitY),
                        Z = Functions.vp_double(sender, Attribute.ClickHitZ)
                    };
            }
            
            OnObjectClick(Implementor,
                          new ObjectClickArgsT<TAvatar,TVpObject, TVector3>
                              {WorldHit=world, Avatar = GetAvatar(session), VpObject = new TVpObject {Id = objectId}});
        }

        private void OnObjectBumpNative(IntPtr sender)
        {
            if (OnObjectBump == null) return;
            int session;
            int objectId;
            TVector3 world;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
            }

            OnObjectBump(Implementor,
                          new ObjectBumpArgsT<TAvatar, TVpObject, TVector3> { BumpType = BumpType.BumpBegin, Avatar = GetAvatar(session), VpObject = new TVpObject { Id = objectId } });
        }

        private void OnObjectBumpEndNative(IntPtr sender)
        {
            if (OnObjectBump == null) return;
            int session;
            int objectId;
            TVector3 world;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
            }

            OnObjectBump(Implementor,
                          new ObjectBumpArgsT<TAvatar, TVpObject, TVector3> { BumpType = BumpType.BumpEnd, Avatar = GetAvatar(session), VpObject = new TVpObject { Id = objectId } });
        }

        private void OnObjectDeleteNative(IntPtr sender)
        {
            if (OnObjectDelete == null) return;
            int session;
            int objectId;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
            }
            OnObjectDelete(Implementor, new ObjectDeleteArgsT<TAvatar, TVpObject, TVector3> { Avatar = GetAvatar(session), VpObject = new TVpObject { Id = objectId } });
        }

        private void OnObjectCreateNative(IntPtr sender)
        {
            if (OnObjectCreate == null && OnQueryCellResult == null) return;
            TVpObject vpObject;
            int session;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                GetVpObject(sender, out vpObject);
            }
            if (session == 0 && OnQueryCellResult != null)
                OnQueryCellResult(Implementor, new QueryCellResultArgsT<TVpObject, TVector3> { VpObject = vpObject });
            else
                if (OnObjectCreate != null)
                OnObjectCreate(Implementor, new ObjectCreateArgsT<TAvatar, TVpObject, TVector3> { Avatar = GetAvatar(session), VpObject = vpObject });
        }



        public List<TAvatar> Avatars()
        {
            return _avatars.Values.ToList();
        } 

        [Obsolete("Objects are not firewalled anymore, so commits are not needed.")]
        public void Commit(TAvatar avatar)
        {
            //lock (_avatars)
            //{
            //    var cache = _avatars[avatar.Session];
            //    cache.CopyFrom(avatar, false);
            //    foreach (var prop in cache.GetType().GetFields())
            //    {
            //        if (prop.FieldType.BaseType!=null && prop.FieldType.BaseType.Name.StartsWith("BaseInstanceT"))
            //            prop.SetValue(cache, this);
            //    }
            //    _avatars[avatar.Session] = cache;
            //}
        }
    
        public TAvatar GetAvatar(int session)
        {
            if (_avatars.ContainsKey(session))
                return _avatars[session];
            var avatar = new TAvatar { Session = session };
            _avatars.Add(session, avatar);
            return avatar;
        }

        private void setAvatar(TAvatar avatar)
        {
            lock (this)
            {
                if (_avatars.ContainsKey(avatar.Session))
                {
                    _avatars[avatar.Session].CopyFrom(avatar, true);
                }
                else
                {
                    _avatars[avatar.Session] = avatar;
                }
            }
        }

        private static void GetVpObject(IntPtr sender, out TVpObject vpObject)
        {

            vpObject = new TVpObject
            {
                Action = Functions.vp_string(sender, Attribute.ObjectAction),
                Description = Functions.vp_string(sender, Attribute.ObjectDescription),
                Id = Functions.vp_int(sender, Attribute.ObjectId),
                Model = Functions.vp_string(sender, Attribute.ObjectModel),
                Data = Functions.GetData(sender, Attributes.ObjectData),

                Rotation = new TVector3
                {
                    X = Functions.vp_double(sender, Attribute.ObjectRotationX),
                    Y = Functions.vp_double(sender, Attribute.ObjectRotationY),
                    Z = Functions.vp_double(sender, Attribute.ObjectRotationZ)
                },

                Time =
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(
                        Functions.vp_int(sender, Attribute.ObjectTime)),
                ObjectType = Functions.vp_int(sender, Attribute.ObjectType),
                Owner = Functions.vp_int(sender, Attribute.ObjectUserId),
                Position = new TVector3
                {
                    X = Functions.vp_double(sender, Attribute.ObjectX),
                    Y = Functions.vp_double(sender, Attribute.ObjectY),
                    Z = Functions.vp_double(sender, Attribute.ObjectZ)
                },
                Angle = Functions.vp_double(sender, Attribute.ObjectRotationAngle)
            };
        }

        private void OnObjectChangeNative(IntPtr sender)
        {
            if (OnObjectChange == null) return;
            TVpObject vpObject;
            int sessionId;
            lock (this)
            {
                GetVpObject(sender,out vpObject);
                sessionId = Functions.vp_int(sender, Attribute.AvatarSession);
            }
            OnObjectChange(Implementor, new ObjectChangeArgsT<TAvatar, TVpObject, TVector3> { Avatar = GetAvatar(sessionId), VpObject = vpObject });
        }

        private void OnQueryCellEndNative(IntPtr sender)
        {
            if (OnQueryCellEnd == null) return;
            int x;
            int z;
            lock (this)
            {
                x = Functions.vp_int(sender, Attribute.CellX);
                z = Functions.vp_int(sender, Attribute.CellZ);
            }
            OnQueryCellEnd(Implementor, new QueryCellEndArgsT<TCell> { Cell = new TCell { X = x, Z = z } });
        }

        private void OnWorldListNative(IntPtr sender)
        {
            if (OnWorldList == null)
                return;

            TWorld data;
            lock (this)
            {
                string worldName = Functions.vp_string(_instance, Attribute.WorldName);
                data = new TWorld
                {
                    Name = worldName,
                    State = (WorldState)Functions.vp_int(_instance, Attribute.WorldState),
                    UserCount = Functions.vp_int(_instance, Attribute.WorldUsers)
                };
            }
            if (_worlds.ContainsKey(data.Name))
                _worlds.Remove(data.Name);
            _worlds.Add(data.Name,data);
            OnWorldList(Implementor, new WorldListEventArgsT<TWorld> { World = data });
        }

        private void OnWorldSettingNativeEvent(IntPtr instance)
        {
            if (!_worlds.ContainsKey(Configuration.World.Name))
            {
                _worlds.Add(Configuration.World.Name,Configuration.World);
            }
            var world = _worlds[Configuration.World.Name];
            var key = Functions.vp_string(instance, Attributes.WorldSettingKey);
            var value = Functions.vp_string(instance, Attributes.WorldSettingValue);
            world.RawAttributes[key] = value;
        }

        private void OnWorldSettingsChangedNativeEvent(IntPtr instance)
        {
            // Initialize World Object Cache if a local object path has been specified and a objectpath is speficied in the world attributes.
            // TODO: some world, such as Test do not specify a objectpath, maybe there's a default search path we dont know of.
            var world = _worlds[Configuration.World.Name];
            if (!string.IsNullOrEmpty(world.LocalCachePath) && world.RawAttributes.ContainsKey("objectpath"))
            {
                ModelCacheProvider = new OpCacheProvider(_worlds[Configuration.World.Name].RawAttributes["objectpath"],world.LocalCachePath);
            }
            if (OnWorldSettingsChanged != null)
                OnWorldSettingsChanged(Implementor, new WorldSettingsChangedEventArgsT<TWorld>() { World = _worlds[Configuration.World.Name]});
        }

        private void OnUniverseDisconnectNative(IntPtr sender)
        {
            if (OnUniverseDisconnect == null) return;
            OnUniverseDisconnect(Implementor, new UniverseDisconnectEventArgsT<TUniverse> { Universe = Universe });
        }

        private void OnWorldDisconnectNative(IntPtr sender)
        {
            if (OnWorldDisconnect == null) return;
            OnWorldDisconnect(Implementor, new WorldDisconnectEventArgsT<TWorld> { World = World });
        }

        private void OnJoinNative(IntPtr sender)
        {
            if (OnJoin == null) return;
            OnJoin(Implementor, new JoinEventArgsT {
                UserId = Functions.vp_int(sender, Attributes.UserId),
                Id = Functions.vp_int(sender, Attributes.JoinId),
                Name = Functions.vp_string(sender, Attributes.JoinName)
            });
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
                OnObjectChangeCallback = null;
                OnObjectDelete = null;
                OnObjectClick = null;
                OnObjectBump = null;
                OnWorldList = null;
                OnWorldDisconnect = null;
                OnWorldSettingsChanged = null;
                OnWorldDisconnect = null;
                OnUniverseDisconnect = null;
                //OnUserAttributes = null;
                OnQueryCellResult = null;
                OnQueryCellEnd = null;
                OnFriendAddCallback = null;
                OnFriendDeleteCallback = null;
                OnFriendsGetCallback = null;
            }
        }

        public void Dispose()
        {
            if (_instance != IntPtr.Zero)
            {
                if (Configuration.IsChildInstance)
                    return;
                Functions.vp_destroy(_instance);
            }

            if (instanceHandle != null)
            {
                instanceHandle.Free();
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Friend Functions

        public TResult GetFriends()
        {
            return new TResult {Rc = Functions.vp_friends_get(_instance)};
        }

        public TResult AddFriendByName(TFriend friend)
        {
            return new TResult { Rc = Functions.vp_friend_add_by_name(_instance,friend.Name) };
        }

        public TResult AddFriendByName(string name)
        {
            return new TResult { Rc = Functions.vp_friend_add_by_name(_instance, name) };
        }

        public TResult DeleteFriendById(int friendId)
        {
            return new TResult { Rc = Functions.vp_friend_delete(_instance,friendId) };
        }

        public TResult DeleteFriendById(TFriend friend)
        {
            return new TResult { Rc = Functions.vp_friend_delete(_instance, friend.Id) };
        }

        #endregion

        #region ITerrainFunctions Implementation

        public TResult TerrianQuery(int tileX, int tileZ, int[,] nodes)
        {
            return new TResult { Rc = Functions.vp_terrain_query(_instance, tileX,tileZ,nodes) };
        }

        public TResult SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells)
        {
            return new TResult { Rc = Functions.vp_terrain_node_set(_instance, tileX, tileZ, nodeX, nodeZ, cells) };

        }

        #endregion

        #region Implementation of IInstanceEvents

        override internal event EventDelegate OnChatNativeEvent;
        override internal event EventDelegate OnAvatarAddNativeEvent;
        override internal event EventDelegate OnAvatarDeleteNativeEvent;
        override internal event EventDelegate OnAvatarChangeNativeEvent;
        override internal event EventDelegate OnAvatarClickNativeEvent;
        override internal event EventDelegate OnWorldListNativeEvent;
        override internal event EventDelegate OnObjectChangeNativeEvent;
        override internal event EventDelegate OnObjectCreateNativeEvent;
        override internal event EventDelegate OnObjectDeleteNativeEvent;
        override internal event EventDelegate OnObjectClickNativeEvent;
        override internal event EventDelegate OnObjectBumpNativeEvent;
        override internal event EventDelegate OnObjectBumpEndNativeEvent;
        override internal event EventDelegate OnQueryCellEndNativeEvent;
        override internal event EventDelegate OnUniverseDisconnectNativeEvent;
        override internal event EventDelegate OnWorldDisconnectNativeEvent;
        override internal event EventDelegate OnTeleportNativeEvent;
        override internal event EventDelegate OnUserAttributesNativeEvent;
        override internal event EventDelegate OnJoinNativeEvent;

        override internal event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        override internal event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        override internal event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        override internal event CallbackDelegate OnObjectGetCallbackNativeEvent;
        override internal event CallbackDelegate OnFriendAddCallbackNativeEvent;
        override internal event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        override internal event CallbackDelegate OnGetFriendsCallbackNativeEvent;

        override internal event CallbackDelegate OnObjectLoadCallbackNativeEvent;
        override internal event CallbackDelegate OnJoinCallbackNativeEvent;
        override internal event CallbackDelegate OnWorldPermissionUserSetCallbackNativeEvent;
        override internal event CallbackDelegate OnWorldPermissionSessionSetCallbackNativeEvent;
        override internal event CallbackDelegate OnWorldSettingsSetCallbackNativeEvent;


        #endregion

        #region Implementation of IAvatarFunctions<out TResult,TAvatar,in TVector3>

        Dictionary<int, TAvatar> IAvatarFunctions<TResult, TAvatar, TVector3>.Avatars { get; set; }

        #endregion
    }
}
