using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseFriendAddCallbackEventArgs : TimedEventArgs, IFriendAddCallbackEventArgs
    {
        public IFriend Friend { get; set; }
    }
}
