using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnFriendDeleteCallback" />
    /// </summary>
    [XmlRoot("OnFriendDeleteCallback", Namespace = Global.XmlNsEvent)]
    public sealed class FriendDeleteCallbackEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendDeleteCallbackEventArgs" /> class.
        /// </summary>
        /// <param name="friend">The deleted friend.</param>
        public FriendDeleteCallbackEventArgs(Friend friend)
        {
            Friend = friend;
        }

        /// <summary>
        ///     Gets the deleted friend.
        /// </summary>
        /// <value>The deleted friend.</value>
        public Friend Friend { get; }
    }
}