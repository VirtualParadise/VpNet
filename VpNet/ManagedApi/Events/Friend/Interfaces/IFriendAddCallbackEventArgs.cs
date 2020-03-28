namespace VpNet.Interfaces
{
    /// <summary>
    /// Friend Add Callback event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    public interface IFriendAddCallbackEventArgs<TFriend> where TFriend : class, IFriend, new()
    {
        /// <summary>
        /// Gets or sets the friend.
        /// </summary>
        /// <value>
        /// The friend.
        /// </value>
        TFriend Friend { get; set; }
    }
}