using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a world setting-change event.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldSettingsChanged", Namespace = Global.XmlNsEvent)]
    public sealed class WorldSettingsChangedEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldSettingsChangedEventArgs" /> class.
        /// </summary>
        public WorldSettingsChangedEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldSettingsChangedEventArgs" /> class.
        /// </summary>
        /// <value>The changed world.</value>
        public WorldSettingsChangedEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets or sets the changed world.
        /// </summary>
        /// <value>The changed world.</value>
        public World World { get; set; }
    }
}
