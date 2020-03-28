using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Chat Message templated interface specifications.
    /// </summary>
    public interface IChatMessage
    {
        [XmlAttribute]
        ChatMessageTypes Type { get; set; }

        Color Color { get; set; }

        [XmlAttribute]
        TextEffectTypes TextEffectTypes { get; set; }

        [XmlAttribute]
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the name for a console message. Note that this name is not always the same as the bot sending the console message.
        /// Bots can advocate sending messages under different names.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        string Name { get; set; }
    }
}