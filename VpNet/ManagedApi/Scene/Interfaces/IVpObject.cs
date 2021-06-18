using System;
using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// VpObject templated interface specifications.
    /// </summary>
    [XmlRoot("vpObject", Namespace = Global.XmlNsScene)]
    public interface IVpObject
    {
        [XmlAttribute]
        int Id { get; set; }

        [XmlAttribute]
        DateTime Time { get; set; }

        [XmlAttribute]
        int Owner { get; set; }

        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }

        [XmlAttribute]
        double Angle { get; set; }

        [XmlAttribute]
        string Action { get; set; }

        [XmlAttribute]
        string Description { get; set; }

        [XmlAttribute]
        int ObjectType { get; set; }

        [XmlAttribute]
        string Model { get; set; }
        
        byte[] Data { get; set; }

        [XmlIgnore]
        Cell Cell { get; }
    }
}