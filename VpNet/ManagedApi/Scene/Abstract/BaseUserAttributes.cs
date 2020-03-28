using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseUserAttributes : IUserAttributes
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public DateTime RegistrationDate { get; set; }
        public TimeSpan OnlineTime { get; set; }
        [XmlAttribute]
        public DateTime LastLogin { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Email { get; set; }
    }
}
