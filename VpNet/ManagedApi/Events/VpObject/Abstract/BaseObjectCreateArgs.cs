using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseObjectCreateArgs<TAvatar, TVpObject> : TimedEventArgs, IObjectCreateArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {

        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }

        protected BaseObjectCreateArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectCreateArgs() { }
    }
}
