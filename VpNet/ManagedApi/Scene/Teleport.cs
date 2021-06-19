using System;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a teleport.
    /// </summary>
    public class Teleport : IEquatable<Teleport>
    {
        /// <summary>
        ///     Gets or sets the target world of the teleport.
        /// </summary>
        /// <value>The target world of the teleport.</value>
        public World World { get; set; }
        
        /// <summary>
        ///     Gets or sets the target avatar of the teleport.
        /// </summary>
        /// <value>The target avatar of the teleport.</value>
        public Avatar Avatar { get; set; }
        
        /// <summary>
        ///     Gets or sets the target position of the teleport.
        /// </summary>
        /// <value>The target position of the teleport.</value>
        public Vector3 Position { get; set; }
        
        /// <summary>
        ///     Gets or sets the target rotation of the teleport.
        /// </summary>
        /// <value>The target rotation of the teleport.</value>
        public Vector3 Rotation { get; set; }

        /// <inheritdoc />
        public bool Equals(Teleport other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(World, other.World)
                && Equals(Avatar, other.Avatar)
                && Position.Equals(other.Position)
                && Rotation.Equals(other.Rotation);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Teleport) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (World != null ? World.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Avatar != null ? Avatar.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Rotation.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Teleport left, Teleport right) => Equals(left, right);

        public static bool operator !=(Teleport left, Teleport right) => !Equals(left, right);
    }
}
