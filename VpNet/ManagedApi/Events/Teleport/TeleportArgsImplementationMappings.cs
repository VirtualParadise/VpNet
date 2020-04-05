using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [Serializable]
    [XmlRoot("OnTeleport", Namespace = Global.XmlNsEvent)]
    public class TeleportEventArgs : Abstract.BaseTeleportEventArgs { }
}
