namespace VpNet.Interfaces
{
    /// <summary>
    /// Teleport event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleportEventArgs
    {
        /// <summary>
        /// Gets or sets the teleport.
        /// </summary>
        /// <value>
        /// The teleport.
        /// </value>
        ITeleport Teleport { get; set; }
    }
}
