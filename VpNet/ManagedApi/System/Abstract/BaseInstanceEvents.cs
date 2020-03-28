using System;
using VpNet.Interfaces;
using VpNet.NativeApi;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseInstanceEvents<TWorld>
        where TWorld : class, IWorld, new()
    {
        internal IntPtr _instance;
        //internal InstanceConfiguration<TWorld> _configuration;
        public InstanceConfiguration<TWorld> Configuration { get; set; }

        #region Implementation of IInstanceEvents

        internal abstract event EventDelegate OnChatNativeEvent;
        internal abstract event EventDelegate OnAvatarAddNativeEvent;
        internal abstract event EventDelegate OnAvatarDeleteNativeEvent;
        internal abstract event EventDelegate OnAvatarChangeNativeEvent;
        internal abstract event EventDelegate OnAvatarClickNativeEvent; 
        internal abstract event EventDelegate OnWorldListNativeEvent;
        internal abstract event EventDelegate OnObjectChangeNativeEvent;
        internal abstract event EventDelegate OnObjectCreateNativeEvent;
        internal abstract event EventDelegate OnObjectDeleteNativeEvent;
        internal abstract event EventDelegate OnObjectClickNativeEvent;
        internal abstract event EventDelegate OnObjectBumpNativeEvent;
        internal abstract event EventDelegate OnObjectBumpEndNativeEvent; 
        internal abstract event EventDelegate OnQueryCellEndNativeEvent;
        internal abstract event EventDelegate OnUniverseDisconnectNativeEvent;
        internal abstract event EventDelegate OnWorldDisconnectNativeEvent;
        internal abstract event EventDelegate OnTeleportNativeEvent;
        internal abstract event EventDelegate OnUserAttributesNativeEvent;
        internal abstract event EventDelegate OnJoinNativeEvent;

        internal abstract event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectGetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnFriendAddCallbackNativeEvent;
        internal abstract event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        internal abstract event CallbackDelegate OnGetFriendsCallbackNativeEvent;

        internal abstract event CallbackDelegate OnObjectLoadCallbackNativeEvent;
        internal abstract event CallbackDelegate OnJoinCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldPermissionUserSetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldPermissionSessionSetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldSettingsSetCallbackNativeEvent;
        #endregion
    }
}