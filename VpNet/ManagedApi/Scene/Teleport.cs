using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Teleport", Namespace = Global.XmlNsScene)]
    public class Teleport<TWorld,TAvatar> : Abstract.BaseTeleport<TWorld,TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar, new()
    {
        public Teleport(){} 
    }
}
