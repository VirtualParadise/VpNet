using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnWorldList" />.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldList", Namespace = Global.XmlNsEvent)]
    public sealed class WorldListEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldDisconnectEventArgs" /> class.
        /// </summary>
        /// <value>The listed world.</value>
        public WorldListEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets the world returned from the list.
        /// </summary>
        /// <value>The listed world.</value>
        public World World { get; set; }
    }
}
