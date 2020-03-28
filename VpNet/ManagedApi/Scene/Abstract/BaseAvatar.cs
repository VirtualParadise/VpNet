using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatar : IAvatar
    {
        [XmlAttribute]
        virtual public DateTime LastChanged { get; set; }
        [XmlAttribute]
        virtual public int UserId { get; set; }
        [XmlAttribute]
        virtual public string Name { get; set; }
        [XmlIgnore]
        virtual public int Session { get;set; }
        [XmlAttribute]
        virtual public int AvatarType { get; set; }
        virtual public Vector3 Position { get; set; }
        virtual public Vector3 Rotation { get; set; } // X pitch, Y yaw
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
