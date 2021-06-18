using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a world disconnection event.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldDisconnect", Namespace = Global.XmlNsEvent)]
    public sealed class WorldDisconnectEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        public WorldDisconnectEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        /// <value>The world involved in the disconnect.</value>
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
