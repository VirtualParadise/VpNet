namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    public sealed class AvatarLeaveEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarEnterEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar which left the world.</param>
        public AvatarLeaveEventArgs(Avatar avatar)
        {
            Avatar = avatar;
        }

        /// <summary>
        ///     Gets the avatar which entered the world.
        /// </summary>
        /// <value>The avatar which left the world.</value>
        public Avatar Avatar { get; }
    }
}