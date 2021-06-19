namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnChatMessage" />.
    /// </summary>
    public sealed class ChatMessageEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChatMessageEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar which sent the chat message.</param>
        /// <param name="chatMessage">The chat message.</param>
        public ChatMessageEventArgs(Avatar avatar, ChatMessage chatMessage)
        {
            Avatar = avatar;
            ChatMessage = chatMessage;
        }

        /// <summary>
        ///     Gets the avatar which sent the chat message.
        /// </summary>
        /// <value>The avatar which sent the chat message.</value>
        public Avatar Avatar { get; }
        
        /// <summary>
        ///     Gets the chat message.
        /// </summary>
        /// <value>The chat message.</value>
        public ChatMessage ChatMessage { get; }
    }
}
