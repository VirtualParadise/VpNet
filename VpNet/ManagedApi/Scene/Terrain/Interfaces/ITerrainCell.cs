using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface ITerrainCell
    {
        [XmlAttribute]
        double Height { get; set; }

        [XmlIgnore]
        ushort Attributes { get; set; }

        [XmlAttribute]
        bool IsHole { get; set; }

        [XmlAttribute]
        TerrainRotation Rotation { get; set; }

        ushort Texture { get; set; }
    }
}