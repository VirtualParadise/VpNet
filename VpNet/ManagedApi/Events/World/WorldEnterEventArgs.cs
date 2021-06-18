using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a world setting-change event.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldEnter", Namespace = Global.XmlNsEvent)]
    public sealed class WorldEnterEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldEnterEventArgs" /> class.
        /// </summary>
        public WorldEnterEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldEnterEventArgs" /> class.
        /// </summary>
        /// <value>The entered world.</value>
        public WorldEnterEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets or sets the entered world.
        /// </summary>
        /// <value>The entered world.</value>
        public World World { get; set; }
    }
}
