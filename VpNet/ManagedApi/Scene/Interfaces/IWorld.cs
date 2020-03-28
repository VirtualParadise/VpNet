using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// World interface specifications
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the numbe rof users that are currently in the world.
        /// </summary>
        /// <value>
        /// The user count.
        /// </value>
        [XmlAttribute]
        int UserCount { get; set; }
        /// <summary>
        /// Gets or sets the state of the world.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [XmlAttribute]
        WorldState State { get; set; }
        /// <summary>
        /// Gets or sets the attributes of the world settings.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        Dictionary<string,string> RawAttributes { get; set; }
        /// <summary>
        /// Gets or sets the local cache path, if this is specified, the SDK will allow the option to
        /// cache models, unzip them and read their contents.
        /// </summary>
        /// <value>
        /// The local cache path.
        /// </value>
        [XmlAttribute]
        string LocalCachePath { get; set; }
    }
}