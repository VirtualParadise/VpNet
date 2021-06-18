using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnAvatarEnter" />.
    /// </summary>
    [XmlRoot("OnAvatarEnter", Namespace = Global.XmlNsEvent)]
    public class AvatarEnterEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarEnterEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar which entered the world.</param>
        public AvatarEnterEventArgs(Avatar avatar)
        {
            Avatar = avatar;
        }

        /// <summary>
        ///     Gets the avatar which entered the world.
        /// </summary>
        /// <value>The avatar which entered the world.</value>
        public Avatar Avatar { get; }
    }
}
