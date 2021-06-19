using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnFriendsGetCallback" />.
    /// </summary>
    [XmlRoot("OnFriendsGetCallback", Namespace = Global.XmlNsEvent)]
    public sealed class FriendsGetCallbackEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendsGetCallbackEventArgs" /> class.
        /// </summary>
        /// <param name="friend">The queried friend.</param>
        public FriendsGetCallbackEventArgs(Friend friend)
        {
            Friend = friend;
        }

        /// <summary>
        ///     Gets the queried friend.
        /// </summary>
        /// <value>The queried friend.</value>
        public Friend Friend { get; }
    }
}