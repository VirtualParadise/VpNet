namespace VpNet
{
    /// <summary>
    ///     Represents a chat message.
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChatMessage" /> class.
        /// </summary>
        /// <param name="name">The name of the message author.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">The message type.</param>
        /// <param name="color">The color of the message.</param>
        /// <param name="textEffectTypes">The text effects of the message.</param>
        public ChatMessage(string name, string message, ChatMessageTypes type, Color color, TextEffectTypes textEffectTypes)
        {
            Color = color;
            TextEffectTypes = textEffectTypes;
            Message = message;
            Name = name;
            Type = type;
        }

        /// <summary>
        ///     Gets the color of the message.
        /// </summary>
        /// <value>The color of the message.</value>
        public Color Color { get; }

        /// <summary>
        ///     Gets the message text.
        /// </summary>
        /// <value>The message text.</value>
        public string Message { get; }

        /// <summary>
        ///     Gets the name of the message author.
        /// </summary>
        /// <value>The name of the message author.</value>
        public string Name { get; }

        /// <summary>
        ///     Gets the text effects of the message.
        /// </summary>
        /// <value>The text effects of the message.</value>
        public TextEffectTypes TextEffectTypes { get; }
        
        /// <summary>
        ///     Gets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public ChatMessageTypes Type { get; }
    }
}
