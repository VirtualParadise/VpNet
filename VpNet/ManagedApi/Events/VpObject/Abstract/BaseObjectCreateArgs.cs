using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseObjectCreateArgs : TimedEventArgs, IObjectCreateArgs
    {
        public IVpObject VpObject { get; set; }
        public IAvatar Avatar { get; set; }

        protected BaseObjectCreateArgs(IAvatar avatar, IVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectCreateArgs() { }
    }
}
