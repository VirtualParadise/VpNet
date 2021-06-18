using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    [XmlRoot("vpObject", Namespace = Global.XmlNsScene)]
    public abstract class BaseVpObject : IVpObject
    {
        private Vector3 _position;
        private Cell? _cell;

        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public DateTime Time { get; set; }
        [XmlAttribute]
        public int Owner { get; set; }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _cell = new Cell();
                _position = value;
            }
        }
        public Vector3 Rotation { get; set; }
        [XmlAttribute]
        public double Angle { get; set; }
        [XmlAttribute]
        public string Action { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public int ObjectType { get; set; }
        [XmlAttribute]
        public string Model { get; set; }

        public byte[] Data { get; set; }
        [XmlIgnore]
        public int ReferenceNumber { get; set; }

        protected BaseVpObject(int id, int objectType, DateTime time, int owner, Vector3 position, Vector3 rotation, double angle, string action, string description, string model, byte[] data)
        {
            Id = id;
            ObjectType = objectType;
            Time = time;
            Owner = owner;
            Position = position;
            Rotation = rotation;
            Angle = angle;
            Action = action;
            Description = description;
            Model = model;
            Data = data;
        }

        protected BaseVpObject()
        {
            Angle = double.MaxValue;
            Time = DateTime.UtcNow;
        }

        [XmlIgnore]
        public Cell Cell
        {
            get
            {
                if (_cell == null)
                   // if (Position != null)
                    _cell = new Cell((int) (Math.Floor(Position.X)/10), (int) (Math.Floor(Position.Z)/10));
                   // else
                   //     _cell = new Cell(0,0);

                return _cell.Value;
            }
        }
    }
}
