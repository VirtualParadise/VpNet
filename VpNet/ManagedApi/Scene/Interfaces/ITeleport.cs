namespace VpNet.Interfaces
{
    /// <summary>
    /// Teleport templated interface specifications.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleport<TWorld, TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar,new()
    {
        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        TWorld World { get; set; }
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        TAvatar Avatar { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Vector3 Position { get; set; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        Vector3 Rotation { get; set; }
    }
}
