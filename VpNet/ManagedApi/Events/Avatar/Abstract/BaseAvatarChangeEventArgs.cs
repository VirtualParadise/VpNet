using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    /// <summary>
    /// Abstract implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [Serializable]
    public abstract class BaseAvatarChangeEventArgs : TimedEventArgs,IAvatarChangeEventArgs
    {
        virtual public IAvatar Avatar { get; set; }
        virtual public IAvatar AvatarPrevious { get; set; }

        public virtual System.TimeSpan TimeSpan
        {
            get
            {
                return Avatar.LastChanged - AvatarPrevious.LastChanged;

            }
        }

        protected BaseAvatarChangeEventArgs(IAvatar avatar, IAvatar avatarPrevious)
        {
            Avatar = avatar;
            AvatarPrevious = avatarPrevious;
        }

        protected BaseAvatarChangeEventArgs() { }
    }
}
