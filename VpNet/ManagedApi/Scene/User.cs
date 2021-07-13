using System;

namespace VpNet
{
    /// <summary>
    ///     Represents a user.
    /// </summary>
    public class User : IEquatable<User>
    {
        internal User()
        {
        }

        /// <summary>
        ///     Gets or sets the email address registered with this user.
        /// </summary>
        /// <value>The email address registered with this user.</value>
        public string Email { get; internal set; }

        /// <summary>
        ///     Gets the ID of this user.
        /// </summary>
        /// <value>The ID of this user.</value>
        public int Id { get; internal set; }
        
        /// <summary>
        ///     Gets the date and time at which this user last logged in.
        /// </summary>
        /// <value>The date and time at which this user last logged in.</value>
        public DateTimeOffset LastLogin { get; internal set; }
        
        /// <summary>
        ///     Gets the name of this user.
        /// </summary>
        /// <value>The name of this user.</value>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets the duration for which this user has been online.
        /// </summary>
        /// <value>The duration for which this user has been online.</value>
        public TimeSpan OnlineTime { get; internal set; }

        /// <summary>
        ///     Gets the date and time at which this user was registered.
        /// </summary>
        /// <value>The date and time at which this user was registered.</value>
        public DateTimeOffset RegistrationDate { get; internal set; }

        /// <inheritdoc />
        public bool Equals(User other)
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
            return Equals((User) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id;

        public static bool operator ==(User left, User right) => Equals(left, right);

        public static bool operator !=(User left, User right) => !Equals(left, right);
    }
}
