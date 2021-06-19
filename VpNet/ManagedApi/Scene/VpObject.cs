using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("VpObject", Namespace = Global.XmlNsScene)]
    public class VpObject : IEquatable<VpObject>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VpObject" /> class.
        /// </summary>
        public VpObject() : this(0)
        {
        }

        internal VpObject(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets or sets the object angle.
        /// </summary>
        /// <value>The object angle.</value>
        public double Angle { get; set; } = double.PositiveInfinity;
        
        /// <summary>
        ///     Gets or sets the object action.
        /// </summary>
        /// <value>The object action.</value>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the cell in which this object is located.
        /// </summary>
        /// <value>The cell  in which this object is located.</value>
        public Cell Cell
        {
            get
            {
                int x = (int) (Math.Floor(Position.X) / 10);
                int z = (int) (Math.Floor(Position.Z) / 10);
                return new Cell(x, z);
            }
        }
        
        /// <summary>
        ///     Gets or sets the object description.
        /// </summary>
        /// <value>The object description.</value>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        ///     Gets or sets the object data.
        /// </summary>
        /// <value>The object data.</value>
        public byte[] Data { get; set; }
        
        /// <summary>
        ///     Gets the object ID.
        /// </summary>
        /// <value>The object ID.</value>
        public int Id { get; internal set; }
        
        /// <summary>
        ///     Gets or sets the object model name.
        /// </summary>
        /// <value>The object model name.</value>
        public string Model { get; set; } = string.Empty;
        
        /// <summary>
        ///     Gets or sets the object type.
        /// </summary>
        /// <value>The object type.</value>
        public int ObjectType { get; set; }
        
        /// <summary>
        ///     Gets the owner of the object.
        /// </summary>
        /// <value>The owner of the object.</value>
        public int Owner { get; set; }
        
        /// <summary>
        ///     Gets or sets the object position.
        /// </summary>
        /// <value>The object position.</value>
        public Vector3 Position { get; set; }
        
        /// <summary>
        ///     Gets or sets the object reference number.
        /// </summary>
        /// <value>The object reference number.</value>
        public int ReferenceNumber { get; set; }
        
        /// <summary>
        ///     Gets or sets the object rotation. 
        /// </summary>
        /// <value>The object rotation.</value>
        public Vector3 Rotation { get; set; }
        
        /// <summary>
        ///     Gets or sets the time at which this object was last modified.
        /// </summary>
        /// <value>The time at which this object was last modified.</value>
        public DateTimeOffset Time { get; set; }

        /// <inheritdoc />
        public bool Equals(VpObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VpObject) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id;

        public static bool operator ==(VpObject left, VpObject right) => Equals(left, right);

        public static bool operator !=(VpObject left, VpObject right) => !Equals(left, right);
    }
}
