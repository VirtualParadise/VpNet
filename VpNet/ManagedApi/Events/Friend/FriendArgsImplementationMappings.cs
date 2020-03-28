using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnFriendAddCallback", Namespace = Global.XmlNsEvent)]
    public class FriendAddCallbackEventArgs : BaseFriendAddCallbackEventArgs<Friend>{}
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnFriendDeleteCallback", Namespace = Global.XmlNsEvent)]
    public class FriendDeleteCallbackEventArgs : BaseFriendDeleteCallbackEventArgs<Friend> { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
     [XmlRoot("OnFriendsGetCallback", Namespace = Global.XmlNsEvent)]
    public class FriendsGetCallbackEventArgs : BaseFriendsGetCallbackEventArgs<Friend> { }
}
