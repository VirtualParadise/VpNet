namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    public sealed class TeleportEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TeleportEventArgs" /> class.
        /// </summary>
        /// <param name="teleport">The teleport information.</param>
        public TeleportEventArgs(Teleport teleport)
        {
            Teleport = teleport;
        }

        /// <summary>
        ///     Gets or sets the teleport information related to this event.
        /// </summary>
        /// <value>The teleport information.</value>
        public Teleport Teleport { get; }
    }
}
