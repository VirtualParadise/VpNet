using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [Serializable]
    [XmlRoot("OnUniverseDisconnect", Namespace = Global.XmlNsEvent)]
    public class UniverseDisconnectEventArgsT<TUniverse> : Abstract.BaseUniverseDisconnectEventArgs
    {}
}
