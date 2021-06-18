﻿using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnWorldSettingsChanged" />.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldSettingsChanged", Namespace = Global.XmlNsEvent)]
    public sealed class WorldSettingsChangedEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldSettingsChangedEventArgs" /> class.
        /// </summary>
        /// <value>The changed world.</value>
        public WorldSettingsChangedEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets the world which was changed.
        /// </summary>
        /// <value>The changed world.</value>
        public World World { get; set; }
    }
}
