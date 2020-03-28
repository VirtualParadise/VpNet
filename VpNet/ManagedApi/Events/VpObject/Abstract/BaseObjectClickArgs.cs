using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectClickArgs<TAvatar, TVpObject> : TimedEventArgs, IObjectClickArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }
        public Vector3 WorldHit { get; set; }

        protected BaseObjectClickArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectClickArgs() { }
    }
}
