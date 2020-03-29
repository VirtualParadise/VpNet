using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseInstanceConfiguration<TWorld>
        where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }
        [XmlAttribute]
        public bool IsChildInstance { get; set; }
        [XmlAttribute]
        public string UserName { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        [XmlAttribute]
        public string BotName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
    }
}
