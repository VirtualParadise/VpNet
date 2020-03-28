using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseFriendAddCallbackEventArgs<TFriend> : TimedEventArgs, IFriendAddCallbackEventArgs<TFriend> where TFriend : class, IFriend, new()
    {
        public TFriend Friend { get; set; }
    }
}
