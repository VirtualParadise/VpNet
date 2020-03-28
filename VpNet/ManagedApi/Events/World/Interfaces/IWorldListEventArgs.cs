namespace VpNet.Interfaces
{
    /// <summary>
    /// World List event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    public interface IWorldListEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        TWorld World { get; set; }
    }
}