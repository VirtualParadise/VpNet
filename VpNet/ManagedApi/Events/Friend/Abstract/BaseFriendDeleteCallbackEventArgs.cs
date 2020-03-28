using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseFriendDeleteCallbackEventArgs<TFriend> : TimedEventArgs, IFriendDeleteCallbackEventArgs<TFriend> where TFriend : class, IFriend, new()
    {
        public TFriend Friend { get; set; }
    }
}
