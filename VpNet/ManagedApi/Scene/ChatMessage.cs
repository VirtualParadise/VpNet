using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("ChatMessage", Namespace = Global.XmlNsScene)]
    public class ChatMessage : Abstract.ChatMessage
    {
        public ChatMessage(string message) : base(message)
        {
        }

        public ChatMessage() { }
    }
}
