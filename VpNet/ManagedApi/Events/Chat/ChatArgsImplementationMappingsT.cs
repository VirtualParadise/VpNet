using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnChatMessage", Namespace = Global.XmlNsEvent)]
    public class ChatMessageEventArgsT<TAvatar, TChatMessage> : Abstract.BaseChatMessageEventArgs
    { }
}
