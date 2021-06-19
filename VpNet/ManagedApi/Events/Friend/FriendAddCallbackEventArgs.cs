namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnFriendAddCallback" />.
    /// </summary>
    public sealed class FriendAddCallbackEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendAddCallbackEventArgs" /> class.
        /// </summary>
        /// <param name="friend">The added friend.</param>
        public FriendAddCallbackEventArgs(Friend friend)
        {
            Friend = friend;
        }

        /// <summary>
        ///     Gets the added friend.
        /// </summary>
        /// <value>The added friend.</value>
        public Friend Friend { get; }
    }
}
