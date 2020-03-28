using System;
using VpNet.Extensions;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarEnterEventArgs<TAvatar> : TimedEventArgs, IAvatarEnterEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    {
        virtual public TAvatar Avatar { get; set; }

        protected BaseAvatarEnterEventArgs(TAvatar avatar)
        {
            Avatar = avatar.Copy();
        }

        protected BaseAvatarEnterEventArgs() { }
    }
}
