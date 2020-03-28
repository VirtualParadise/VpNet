using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// World Cell specification
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>
        /// The X.
        /// </value>
        [XmlAttribute]
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>
        /// The Z.
        /// </value>
        [XmlAttribute]
        int Z { get; set; }
    }
}