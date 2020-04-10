namespace VpNet.Interfaces
{
    /// <summary>
    /// Friend Add Callback event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    public interface IFriendAddCallbackEventArgs
    {
        /// <summary>
        /// Gets or sets the friend.
        /// </summary>
        /// <value>
        /// The friend.
        /// </value>
        IFriend Friend { get; set; }
    }
}