using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseChatMessageEventArgs<TAvatar, TChatMessage> : TimedEventArgs, 
        IChatMessageEventArgs<TAvatar, TChatMessage>
        where TAvatar : class, IAvatar,new()
        where TChatMessage : class, IChatMessage,new()
    {
        public TAvatar Avatar { get; set; }
        public TChatMessage ChatMessage {get;set;}

        protected BaseChatMessageEventArgs(TAvatar avatar, TChatMessage chatMessage)
        {
            Avatar = avatar;
            ChatMessage = chatMessage;
        }

        protected BaseChatMessageEventArgs() { }
    }
}
