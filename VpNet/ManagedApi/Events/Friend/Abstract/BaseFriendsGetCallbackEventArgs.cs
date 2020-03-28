using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public class BaseFriendsGetCallbackEventArgs<TFriend> : TimedEventArgs, IFriendsGetCallbackEventArgs<TFriend> where TFriend : class, IFriend, new()
    {
        public TFriend Friend { get; set; }
    }
}
