namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnWorldLeave" />.
    /// </summary>
    public sealed class WorldLeaveEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldLeaveEventArgs" /> class.
        /// </summary>
        /// <value>The left world.</value>
        public WorldLeaveEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets the world which was left.
        /// </summary>
        /// <value>The world which was left.</value>
        public World World { get; }
    }
}
