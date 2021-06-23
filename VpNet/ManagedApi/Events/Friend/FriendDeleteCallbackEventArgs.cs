using System;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.OnFriendDeleteCallback" />
    /// </summary>
    public sealed class FriendDeleteCallbackEventArgs : EventArgs
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