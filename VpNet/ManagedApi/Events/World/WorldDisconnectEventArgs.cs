namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnWorldDisconnect" />.
    /// </summary>
    public sealed class WorldDisconnectEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        /// <value>The disconnected world.</value>
        public WorldDisconnectEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets or sets the disconnected world.
        /// </summary>
        /// <value>The disconnected world.</value>
        public World World { get; set; }
    }
}
