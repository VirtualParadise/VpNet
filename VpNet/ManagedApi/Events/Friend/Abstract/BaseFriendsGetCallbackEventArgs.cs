using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public class BaseFriendsGetCallbackEventArgs : TimedEventArgs, IFriendsGetCallbackEventArgs
    {
        public IFriend Friend { get; set; }
    }
}
