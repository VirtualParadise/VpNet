using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarLeaveEventArgs : TimedEventArgs, IAvatarLeaveEventArgs
    {
        virtual public IAvatar Avatar { get; set; }

        protected BaseAvatarLeaveEventArgs(IAvatar avatar)
        {
            Avatar = avatar;
        }

        protected BaseAvatarLeaveEventArgs() { }
    }
}
