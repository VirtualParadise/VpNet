using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarLeaveEventArgs<TAvatar> : TimedEventArgs, IAvatarLeaveEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    {
        virtual public TAvatar Avatar { get; set; }

        protected BaseAvatarLeaveEventArgs(TAvatar avatar)
        {
            Avatar = avatar;
        }

        protected BaseAvatarLeaveEventArgs() { }
    }
}
