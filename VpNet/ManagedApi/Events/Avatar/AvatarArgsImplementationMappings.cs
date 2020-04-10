using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnAvatarChange", Namespace = Global.XmlNsEvent)]
    public class AvatarChangeEventArgs : Abstract.BaseAvatarChangeEventArgs{ }

    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnAvatarEnter", Namespace = Global.XmlNsEvent)]
    public class AvatarEnterEventArgs : Abstract.BaseAvatarEnterEventArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnAvatarLeave", Namespace = Global.XmlNsEvent)]
    public class AvatarLeaveEventArgs : Abstract.BaseAvatarLeaveEventArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnAvatarClick", Namespace = Global.XmlNsEvent)]
    public class AvatarClickEventArgs : Abstract.BaseAvatarClickEventArgs { }

    [XmlRoot("OnJoin", Namespace = Global.XmlNsEvent)]
    public class JoinEventArgs : Abstract.BaseJoinEventArgs { }

}
