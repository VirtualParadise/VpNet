using System;

namespace VpNet
{
    public class VpObject : IEquatable<VpObject>
    {
        /// <summary>
        ///     Gets the object angle.
        /// </summary>
        /// <value>The object angle.</value>
        public double Angle { get; internal set; } = double.PositiveInfinity;

        /// <summary>
        ///     Gets the <c>Action</c> field of this object.
        /// </summary>
        /// <value>The value of the <c>Action</c> field.</value>
        public string Action { get; internal set; } = string.Empty;

        /// <summary>
        ///     Gets the cell in which this object is located.
        /// </summary>
        /// <value>The cell in which this object is located.</value>
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
        ///     Gets the <c>Description</c> field of this object.
        /// </summary>
        /// <value>The value of the <c>Description</c> field.</value>
        public string Description { get; internal set; } = string.Empty;

        /// <summary>
        ///     Gets the object data.
        /// </summary>
        /// <value>The object data.</value>
        public byte[] Data { get; internal set; }

        /// <summary>
        ///     Gets the object ID.
        /// </summary>
        /// <value>The object ID.</value>
        public int Id { get; internal set; }

        /// <summary>
        ///     Gets the <c>Model</c> field of this object.
        /// </summary>
        /// <value>The value of the <c>Model</c> field.</value>
        public string Model { get; internal set; } = string.Empty;

        /// <summary>
        ///     Gets the owner of the object.
        /// </summary>
        /// <value>The owner of the object.</value>
        public int Owner { get; internal set; }

        /// <summary>
        ///     Gets the position of this object.
        /// </summary>
        /// <value>A <see cref="Vector3" /> representing the position.</value>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Gets the rotation of this object.
        /// </summary>
        /// <value>A <see cref="Vector3" /> representing the rotation.</value>
        public Vector3 Rotation { get; internal set; }

        /// <summary>
        ///     Gets the date and time at which this object was last modified.
        /// </summary>
        /// <value>The <see cref="DateTimeOffset" /> representing the date and time at which this object was last modified.</value>
        public DateTimeOffset Time { get; internal set; }

        /// <summary>
        ///     Gets the object type.
        /// </summary>
        /// <value>The object type.</value>
        public int Type { get; internal set; }

        internal int ReferenceNumber { get; set; }

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
