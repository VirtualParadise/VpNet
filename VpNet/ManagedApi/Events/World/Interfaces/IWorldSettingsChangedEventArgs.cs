namespace VpNet.Interfaces
{
    /// <summary>
    /// World settings changed event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    public interface IWorldSettingsChangedEventArgs
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