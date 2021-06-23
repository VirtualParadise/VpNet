namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.AvatarEntered" />.
    /// </summary>
    public sealed class AvatarEnterEventArgs : TimedEventArgs
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
