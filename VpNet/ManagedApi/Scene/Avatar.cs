using System;

namespace VpNet
{
    /// <summary>
    ///     Represents an avatar; an online representation of a user.
    /// </summary>
    public class Avatar : IEquatable<Avatar>, ICloneable
    {
        internal Avatar()
        {
        }

        /// <summary>
        ///     Gets the name of the application this avatar client is using.
        /// </summary>
        /// <value>The application name.</value>
        public string ApplicationName { get; internal set; }
        
        /// <summary>
        ///     Gets the version of the application this avatar client is using.
        /// </summary>
        /// <value>The application version.</value>
        public string ApplicationVersion { get; internal set; }
        
        /// <summary>
        ///     Gets the type of this avatar.
        /// </summary>
        /// <value>The type of this avatar.</value>
        public int AvatarType { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this avatar is a bot.
        /// </summary>
        /// <value><see langword="true" /> if this avatar is a bot; otherwise <see langword="false" />.</value>
        public bool IsBot => !string.IsNullOrWhiteSpace(Name) && Name[0] == '[' && Name[Name.Length - 1] == ']';
        
        /// <summary>
        ///     Gets the time at which this avatar was last updated. 
        /// </summary>
        /// <value>The time at which this avatar was last updated.</value>
        public DateTimeOffset LastChanged { get; internal set; }
        
        /// <summary>
        ///     Gets the location of this avatar.
        /// </summary>
        /// <value>The location of this avatar.</value>
        public Location Location { get; internal set; }
        
        /// <summary>
        ///     Gets the name of this avatar.
        /// </summary>
        /// <value>The name of this avatar.</value>
        public string Name { get; internal set; }
        
        /// <summary>
        ///     Gets the session of this avatar.
        /// </summary>
        /// <value>The session of this avatar.</value>
        public int Session { get; internal set; }
        
        /// <summary>
        ///     Gets the user associated with this avatar.
        /// </summary>
        /// <value>The user associated with this avatar.</value>
        public User User { get; internal set; }

        /// <inheritdoc />
        public bool Equals(Avatar other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Session == other.Session && User == other.User;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Avatar) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Session * 397) ^ User.Id;
            }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return new Avatar
            {
                Session = Session,
                Name = Name,
                AvatarType = AvatarType,
                Location = Location,
                LastChanged = LastChanged,
                ApplicationName = ApplicationName,
                ApplicationVersion = ApplicationVersion,
                User = User
            };
        }

        public static bool operator ==(Avatar left, Avatar right) => Equals(left, right);

        public static bool operator !=(Avatar left, Avatar right) => !Equals(left, right);
    }
}
