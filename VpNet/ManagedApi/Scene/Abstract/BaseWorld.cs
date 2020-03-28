using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorld : IWorld
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int UserCount { get; set; }
        [XmlAttribute]
        public WorldState State { get; set; }
        public Dictionary<string, string> RawAttributes { get; set; }
        [XmlAttribute]
        public string LocalCachePath { get; set; }

        protected BaseWorld()
        {
            RawAttributes = new Dictionary<string, string>();
        }
    }
}
