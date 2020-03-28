using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("World", Namespace = Global.XmlNsScene)]
    public class World : Abstract.BaseWorld
    {
    }
}
