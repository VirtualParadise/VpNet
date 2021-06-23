namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.OnTeleport" />.
    /// </summary>
    public sealed class TeleportEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TeleportEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar sending the teleport.</param>
        /// <param name="location">The location of the teleport.</param>
        public TeleportEventArgs(Avatar avatar, Location location)
        {
            Avatar = avatar;
            Location = location;
        }
        
        /// <summary>
        ///     Gets the avatar sending the teleport.
        /// </summary>
        /// <value>The avatar sending the teleport.</value>
        public Avatar Avatar { get; set; }
        
        /// <summary>
        ///     Gets the location of the teleport.
        /// </summary>
        /// <value>The location of the teleport.</value>
        public Location Location { get; set; }
    }
}
