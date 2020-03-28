using System;
using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Friend", Namespace = Global.XmlNsScene)]
    public class Friend : BaseFriend
    {
    }
}