using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseChatMessageEventArgs : TimedEventArgs, IChatMessageEventArgs
    {
        public IAvatar Avatar { get; set; }
        public IChatMessage ChatMessage {get;set;}

        protected BaseChatMessageEventArgs(IAvatar avatar, IChatMessage chatMessage)
        {
            Avatar = avatar;
            ChatMessage = chatMessage;
        }

        protected BaseChatMessageEventArgs() { }
    }
}
