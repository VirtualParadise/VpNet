using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectDeleteArgs<TAvatar, TVpObject> : TimedEventArgs, IObjectDeleteArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }

        protected BaseObjectDeleteArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectDeleteArgs() { }
    }
}
