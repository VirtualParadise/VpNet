using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Templated Event Arguments implementation.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [XmlRoot("OnAvatarChange", Namespace = Global.XmlNsEvent)]
    public class AvatarChangeEventArgsT<TAvatar> : Abstract.BaseAvatarChangeEventArgs<TAvatar>
       where TAvatar : class, IAvatar, new()
    { }

    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [XmlRoot("OnAvatarEnter", Namespace = Global.XmlNsEvent)]
    public class AvatarEnterEventArgsT<TAvatar> : Abstract.BaseAvatarEnterEventArgs<TAvatar>
       where TAvatar : class, IAvatar, new()
    { }
    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [XmlRoot("OnAvatarLeave", Namespace = Global.XmlNsEvent)]
    public class AvatarLeaveEventArgsT<TAvatar> : Abstract.BaseAvatarLeaveEventArgs<TAvatar>
       where TAvatar : class, IAvatar, new()
    { }
    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    [XmlRoot("OnAvatarClick", Namespace = Global.XmlNsEvent)]
    public class AvatarClickEventArgsT<TAvatar> : Abstract.BaseAvatarClickEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    { }

    [XmlRoot("OnJoin", Namespace = Global.XmlNsEvent)]
    public class JoinEventArgsT : Abstract.BaseJoinEventArgs { }
}
