namespace VpNet.Interfaces
{
    /// <summary>
    /// World settings changed event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    public interface IWorldSettingsChangedEventArgs<TWorld> where TWorld : class, IWorld, new()
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