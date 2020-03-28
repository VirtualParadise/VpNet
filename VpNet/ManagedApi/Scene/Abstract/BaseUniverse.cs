using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseUniverse : IUniverse
    {
        [XmlAttribute]
        public string Host { get; set; }
        [XmlAttribute]
        public int Port { get; set; }
    }
}