using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a world list event.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldList", Namespace = Global.XmlNsEvent)]
    public sealed class WorldListEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        public WorldListEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        /// <value>The listed world.</value>
        public WorldListEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets or sets the listed world.
        /// </summary>
        /// <value>The listed world.</value>
        public World World { get; set; }
    }
}
