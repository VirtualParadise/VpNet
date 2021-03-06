namespace VpNet.Interfaces
{
    /// <summary>
    /// Friends Get Callback event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    public interface IFriendsGetCallbackEventArgs
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