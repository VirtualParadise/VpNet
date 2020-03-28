using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    /// <summary>
    /// Abstract implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [Serializable]
    public abstract class BaseAvatarChangeEventArgs<TAvatar> : TimedEventArgs,IAvatarChangeEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    {
        virtual public TAvatar Avatar { get; set; }
        virtual public TAvatar AvatarPrevious { get; set; }

        public virtual System.TimeSpan TimeSpan
        {
            get
            {
                return Avatar.LastChanged - AvatarPrevious.LastChanged;

            }
        }

        protected BaseAvatarChangeEventArgs(TAvatar avatar, TAvatar avatarPrevious)
        {
            Avatar = avatar;
            AvatarPrevious = avatarPrevious;
        }

        protected BaseAvatarChangeEventArgs() { }
    }
}
