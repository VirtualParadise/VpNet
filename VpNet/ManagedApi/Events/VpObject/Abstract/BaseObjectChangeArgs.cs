using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    /// <summary>
    /// Called when an object is changed in the world by another user.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    [Serializable]
    public abstract class BaseObjectChangeArgs<TAvatar, TVpObject> : TimedEventArgs, IObjectChangeArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }

        protected BaseObjectChangeArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectChangeArgs() { }
    }
}
