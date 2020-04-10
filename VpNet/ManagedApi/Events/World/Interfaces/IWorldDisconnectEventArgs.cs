namespace VpNet.Interfaces
{
    /// <summary>
    /// World Disconnected event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    public interface IWorldDisconnectEventArgs
    {
        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        IWorld World { get; set; }
    }
}