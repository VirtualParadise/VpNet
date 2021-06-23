namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.OnJoin" />.
    /// </summary>
    public sealed class JoinEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JoinEventArgs" /> class.
        /// </summary>
        /// <param name="joinRequest">The incoming join request.</param>
        public JoinEventArgs(JoinRequest joinRequest)
        {
            Request = joinRequest;
        }

        /// <summary>
        ///     Gets the join request.
        /// </summary>
        /// <value>The join request.</value>
        public JoinRequest Request { get; }
    }
}