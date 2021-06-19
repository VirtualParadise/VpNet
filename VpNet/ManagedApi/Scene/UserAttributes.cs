using System;

namespace VpNet
{
    /// <summary>
    ///     Represents user attributes.
    /// </summary>
    public class UserAttributes
    {
        internal UserAttributes(int id, string name, string email, DateTime lastLogin, TimeSpan onlineTime, DateTime registrationDate)
        {
            Id = id;
            RegistrationDate = registrationDate;
            OnlineTime = onlineTime;
            LastLogin = lastLogin;
            Name = name;
            Email = email;
        }

        /// <summary>
        ///     Gets or sets the email address registered with this user.
        /// </summary>
        /// <value>The email address registered with this user.</value>
        public string Email { get; set; }

        /// <summary>
        ///     Gets the ID of the user.
        /// </summary>
        /// <value>The ID of the user.</value>
        /// <remarks>
        ///     This value should not be mistaken with the avatar session, which serves to be a world connection identifier. User
        ///     ID is persistent for the account itself.
        /// </remarks>
        public int Id { get; set; }

        /// <summary>
        ///     Gets the date and time at which this user was previously logged in.
        /// </summary>
        /// <value>The date and time at which this user was previously logged in.</value>
        public DateTime LastLogin { get; set; }

        /// <summary>
        ///     Gets the name of this user.
        /// </summary>
        /// <value>The name of this user.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the date and time at which this user was registered.
        /// </summary>
        /// <value>The date and time at which this user was registered.</value>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        ///     Gets the duration for which this user has been online.
        /// </summary>
        /// <value>The duration for which this user has been online.</value>
        public TimeSpan OnlineTime { get; set; }
    }
}
