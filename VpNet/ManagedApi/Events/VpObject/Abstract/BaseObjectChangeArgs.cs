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
    public abstract class BaseObjectChangeArgs : TimedEventArgs, IObjectChangeArgs
    {
        public IVpObject VpObject { get; set; }
        public IAvatar Avatar { get; set; }

        protected BaseObjectChangeArgs(IAvatar avatar, IVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectChangeArgs() { }
    }
}
