using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseObjectDeleteArgs : TimedEventArgs, IObjectDeleteArgs
    {
        public IVpObject VpObject { get; set; }
        public IAvatar Avatar { get; set; }

        protected BaseObjectDeleteArgs(IAvatar avatar, IVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectDeleteArgs() { }
    }
}
