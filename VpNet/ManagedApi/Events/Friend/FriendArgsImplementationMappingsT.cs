using System.Xml.Serialization;
using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnFriendAddCallback", Namespace = Global.XmlNsEvent)]
    public class FriendAddCallbackEventArgsT<TFriend> : BaseFriendAddCallbackEventArgs<TFriend>
        where TFriend : class, IFriend,new()
    {}
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnFriendDeleteCallback", Namespace = Global.XmlNsEvent)]
    public class FriendDeleteCallbackEventArgsT<TFriend> : BaseFriendDeleteCallbackEventArgs<TFriend>
            where TFriend : class, IFriend, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnFriendsGetCallback", Namespace = Global.XmlNsEvent)]
    public class FriendsGetCallbackEventArgsT<TFriend> : BaseFriendsGetCallbackEventArgs<TFriend>
            where TFriend : class, IFriend, new()
    { }
}
