using System.Xml.Serialization;

namespace VpNet.Abstract
{
    public interface ITerrain
    {
        [XmlAttribute]
        ushort Data { get; set; }
        [XmlAttribute]
        int NodeX { get; set; }
        [XmlAttribute]
        int NodeZ { get; set; }
        [XmlAttribute]
        int NodeRevision { get; set; }
        [XmlAttribute]
        int TileX { get; set; }
        [XmlAttribute]
        int TileZ { get; set; }
    }
}