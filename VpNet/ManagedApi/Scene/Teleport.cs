using System;

namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains information about a teleport.
    /// </summary>
    public class Teleport : IEquatable<Teleport>
    {
        /// <summary>
        ///     Gets or sets the avatar sending the teleport.
        /// </summary>
        /// <value>The avatar sending the teleport.</value>
        public Avatar Avatar { get; set; }
        
        /// <summary>
        ///     Gets or sets the location of the teleport.
        /// </summary>
        /// <value>The location of the teleport.</value>
        public Location Location { get; set; }

        /// <inheritdoc />
        public bool Equals(Teleport other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Avatar, other.Avatar) && Location.Equals(other.Location);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Teleport other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Avatar != null ? Avatar.GetHashCode() : 0) * 397) ^ Location.GetHashCode();
            }
        }

        public static bool operator ==(Teleport left, Teleport right) => Equals(left, right);

        public static bool operator !=(Teleport left, Teleport right) => !Equals(left, right);
    }
}
