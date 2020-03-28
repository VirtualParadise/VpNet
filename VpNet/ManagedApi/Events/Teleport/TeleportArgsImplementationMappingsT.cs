using System;
using System.Xml.Serialization;
using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [Serializable]
    [XmlRoot("OnTeleport", Namespace = Global.XmlNsEvent)]
    public class TeleportEventArgsT<TTeleport,TWorld,TAvatar> : BaseTeleportEventArgs<TTeleport,TWorld,TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar, new()
        where TTeleport : class, ITeleport<TWorld, TAvatar>, new()
    {}
}
