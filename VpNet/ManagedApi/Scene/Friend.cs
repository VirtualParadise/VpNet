using System;
using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    ///     Represents a friend.
    /// </summary>
    [Serializable]
    [XmlRoot("Friend", Namespace = Global.XmlNsScene)]
    public class Friend
    {
        internal Friend(int userId, string name, bool isOnline)
        {
            UserId = userId;
            Name = name;
            IsOnline = isOnline;
        }

        /// <summary>
        ///     Gets a value indicating whether this friend is online.
        /// </summary>
        /// <value><see langword="true" /> if this friend is online; otherwise <see langword="false" />.</value>
        public bool IsOnline { get; }

        /// <summary>
        ///     Gets the name of this friend.
        /// </summary>
        /// <value>The name of this friend.</value>
        public string Name { get; }
        
        /// <summary>
        ///     Gets the user ID of this friend.
        /// </summary>
        /// <value>The user ID of this friend.</value>
        public int UserId { get; }
    }
}