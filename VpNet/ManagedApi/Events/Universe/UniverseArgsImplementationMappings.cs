using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
   [Serializable]
   [XmlRoot("OnUniverseDisconnect", Namespace = Global.XmlNsEvent)]
    public class UniverseDisconnectEventArgs : Abstract.BaseUniverseDisconnectEventArgs<Universe>{}
}
