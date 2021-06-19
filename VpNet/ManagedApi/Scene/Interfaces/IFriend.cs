using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Friend interface specification
    /// </summary>
    public interface IFriend
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        int UserId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IFriend"/> is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if online; otherwise, <c>false</c>.
        /// </value>
        bool Online { get; set; }

    }
}