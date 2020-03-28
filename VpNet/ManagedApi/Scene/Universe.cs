using System;
using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Universe", Namespace = Global.XmlNsScene)]
    public class Universe: BaseUniverse
    {
    }
}