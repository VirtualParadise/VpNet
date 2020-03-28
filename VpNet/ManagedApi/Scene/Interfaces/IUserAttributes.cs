using System;

namespace VpNet.Interfaces
{
    public interface IUserAttributes
    {
        /// <summary>
        /// Gets or sets the id of the user, not to be mistaken with session.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; set; }
        /// <summary>
        /// Gets or sets the registration date of the user.
        /// </summary>
        /// <value>
        /// The registration date.
        /// </value>
        DateTime RegistrationDate { get; set; }
        /// <summary>
        /// Gets or sets the online time spend by the user in the universe.
        /// </summary>
        /// <value>
        /// The online time.
        /// </value>
        TimeSpan OnlineTime { get; set; }
        /// <summary>
        /// Gets or sets the last login of the user in the universe.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        DateTime LastLogin { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }

    }
}
