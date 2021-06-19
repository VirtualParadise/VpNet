using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatar : IAvatar
    {
        [XmlAttribute]
        public virtual DateTime LastChanged { get; set; }
        [XmlAttribute]
        public virtual int UserId { get; set; }
        [XmlAttribute]
        public virtual string Name { get; set; }
        [XmlIgnore]
        public virtual int Session { get;set; }
        [XmlAttribute]
        public virtual int AvatarType { get; set; }
        public virtual Vector3 Position { get; set; }
        public virtual Vector3 Rotation { get; set; } // X pitch, Y yaw
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }

        [XmlIgnore]
        public virtual bool IsBot
        {
            get
            {
                return Name != null && Name.StartsWith("[");
            }
        }

        protected BaseAvatar(int userId, string name,int session,int avatarType,Vector3 position,Vector3 rotation)
        {
            UserId = userId;
            Name = name;
            Session = session;
            AvatarType = avatarType;
            Position = position;
            Rotation = rotation;
        }

        protected BaseAvatar() { }
    }
}
