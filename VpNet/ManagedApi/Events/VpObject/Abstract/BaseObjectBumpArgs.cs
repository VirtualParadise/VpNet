using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectBumpArgs : TimedEventArgs, IObjectBumpArgs
    {
        public IVpObject VpObject { get; set; }
        public IAvatar Avatar { get; set; }
        public BumpType BumpType { get; set; }

        protected BaseObjectBumpArgs(IAvatar avatar, IVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectBumpArgs() { }
    }
}
