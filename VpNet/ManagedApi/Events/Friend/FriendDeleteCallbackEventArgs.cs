namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnFriendDeleteCallback" />
    /// </summary>
    public sealed class FriendDeleteCallbackEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendDeleteCallbackEventArgs" /> class.
        /// </summary>
        /// <param name="friend">The deleted friend.</param>
        public FriendDeleteCallbackEventArgs(Friend friend)
        {
            Friend = friend;
        }

        /// <summary>
        ///     Gets the deleted friend.
        /// </summary>
        /// <value>The deleted friend.</value>
        public Friend Friend { get; }
    }
}