namespace VpNet.Interfaces
{
    /// <summary>
    /// Teleport event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleportEventArgs<TTeleport, TWorld, TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar,  new()
        where TTeleport : class, ITeleport<TWorld, TAvatar>, new()
    {
        /// <summary>
        /// Gets or sets the teleport.
        /// </summary>
        /// <value>
        /// The teleport.
        /// </value>
        TTeleport Teleport { get; set; }
    }
}
