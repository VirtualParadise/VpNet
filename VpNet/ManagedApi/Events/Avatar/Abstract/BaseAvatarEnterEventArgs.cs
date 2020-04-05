using System;
using VpNet.Extensions;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarEnterEventArgs : TimedEventArgs, IAvatarEnterEventArgs
    {
        virtual public IAvatar Avatar { get; set; }

        protected BaseAvatarEnterEventArgs(IAvatar avatar)
        {
            Avatar = avatar.Copy();
        }

        protected BaseAvatarEnterEventArgs() { }
    }
}
