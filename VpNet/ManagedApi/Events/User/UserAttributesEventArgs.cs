namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.UserAttributesReceived" />.
    /// </summary>
    public sealed class UserAttributesEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAttributesEventArgs" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserAttributesEventArgs(User user)
        {
            User = user;
        }

        /// <summary>
        ///     Gets the user attributes.
        /// </summary>
        /// <value>The user attributes.</value>
        public User User { get; }
    }
}
