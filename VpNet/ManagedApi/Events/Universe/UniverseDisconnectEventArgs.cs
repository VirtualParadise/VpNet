using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnUniverseDisconnect" />.
    /// </summary>
    [Serializable]
    [XmlRoot("OnUniverseDisconnect", Namespace = Global.XmlNsEvent)]
    public sealed class UniverseDisconnectEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UniverseDisconnectEventArgs" /> class.
        /// </summary>
        /// <param name="universe">The disconnected universe.</param>
        public UniverseDisconnectEventArgs(Universe universe) : this(universe, DisconnectType.ServerDisconnected)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UniverseDisconnectEventArgs" /> class.
        /// </summary>
        /// <param name="universe">The disconnected universe.</param>
        /// <param name="disconnectType">The type of the disconnect.</param>
        public UniverseDisconnectEventArgs(Universe universe, DisconnectType disconnectType)
        {
            DisconnectType = disconnectType;
            Universe = universe;
        }

        /// <summary>
        ///     Gets the type of the disconnect.
        /// </summary>
        /// <value>The type of the disconnect.</value>
        public DisconnectType DisconnectType { get; }
        
        /// <summary>
        ///     Gets the disconnected universe.
        /// </summary>
        /// <value>The disconnected universe.</value>
        public Universe Universe { get; }
    }
}
