using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectBumpArgs<TAvatar, TVpObject> : TimedEventArgs, IObjectBumpArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }
        public BumpType BumpType { get; set; }

        protected BaseObjectBumpArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectBumpArgs() { }
    }
}
