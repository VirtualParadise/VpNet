using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a world setting-change event.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldLeave", Namespace = Global.XmlNsEvent)]
    public sealed class WorldLeaveEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldLeaveEventArgs" /> class.
        /// </summary>
        public WorldLeaveEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldLeaveEventArgs" /> class.
        /// </summary>
        /// <value>The left world.</value>
        public WorldLeaveEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets or sets the left world.
        /// </summary>
        /// <value>The left world.</value>
        public World World { get; set; }
    }
}
