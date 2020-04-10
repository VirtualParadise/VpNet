namespace VpNet.Interfaces
{
    /// <summary>
    /// Universe Disconnected event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TUniverse">The type of the universe.</typeparam>
    public interface IUniverseDisconnectEventArgs
    {
        /// <summary>
        /// Gets or sets the universe.
        /// </summary>
        /// <value>
        /// The universe.
        /// </value>
        IUniverse Universe { get; set; }

        /// <summary>
        /// Gets or sets the type of the disconnect.
        /// </summary>
        /// <value>
        /// The type of the disconnect.
        /// </value>
        DisconnectType DisconnectType { get; set; }
    }
}