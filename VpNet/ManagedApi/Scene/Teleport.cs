using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Teleport", Namespace = Global.XmlNsScene)]
    public class Teleport : Abstract.BaseTeleport
    {
        public Teleport(){} 
    }
}
