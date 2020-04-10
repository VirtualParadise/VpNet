namespace VpNet.Interfaces
{
    /// <summary>
    /// Chat Message event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TChatMessage">The type of the chat message.</typeparam>
    public interface IChatMessageEventArgs
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        IAvatar Avatar { get; set; }
        /// <summary>
        /// Gets or sets the chat message.
        /// </summary>
        /// <value>
        /// The chat message.
        /// </value>
        IChatMessage ChatMessage { get; set; }
    }
}