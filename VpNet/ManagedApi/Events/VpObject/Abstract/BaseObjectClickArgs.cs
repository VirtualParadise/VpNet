using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectClickArgs: TimedEventArgs, IObjectClickArgs
    {
        public IVpObject VpObject { get; set; }
        public IAvatar Avatar { get; set; }
        public Vector3 WorldHit { get; set; }

        protected BaseObjectClickArgs(IAvatar avatar, IVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectClickArgs() { }
    }
}
