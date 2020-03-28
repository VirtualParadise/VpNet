using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Universe interface specification
    /// </summary>
    public interface IUniverse
    {
        [XmlAttribute]
        string Host { get; set; }
        [XmlAttribute]
        int Port { get; set; }
    }
}