﻿using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnWorldEnter" />.
    /// </summary>
    [Serializable]
    [XmlRoot("OnWorldEnter", Namespace = Global.XmlNsEvent)]
    public sealed class WorldEnterEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldEnterEventArgs" /> class.
        /// </summary>
        /// <value>The entered world.</value>
        public WorldEnterEventArgs(World world)
        {
            World = world;
        }

        /// <summary>
        ///     Gets the world which was entered.
        /// </summary>
        /// <value>The world which was entered.</value>
        public World World { get; set; }
    }
}
