using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnAvatarChange" />.
    /// </summary>
    [XmlRoot("OnAvatarChange", Namespace = Global.XmlNsEvent)]
    public sealed class AvatarChangeEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarChangeEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The new state of the avatar.</param>
        /// <param name="oldAvatar">The old state of the avatar.</param>
        public AvatarChangeEventArgs(Avatar avatar, Avatar oldAvatar)
        {
            Avatar = avatar;
            OldAvatar = oldAvatar;
        }

        /// <summary>
        ///     Gets the state of the avatar after to the change.
        /// </summary>
        /// <value>The state of the avatar after to the change.</value>
        public Avatar Avatar { get; }

        /// <summary>
        ///     Gets the time difference since the last update of the avatar.
        /// </summary>
        /// <value>The time difference.</value>
        public TimeSpan DeltaTime => Avatar.LastChanged - OldAvatar.LastChanged;

        /// <summary>
        ///     Gets the state of the avatar prior to the change.
        /// </summary>
        /// <value>The state of the avatar prior to the change.</value>
        public Avatar OldAvatar { get; }
    }
}
