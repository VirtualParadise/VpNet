using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("OnWorldDisconnect", Namespace = Global.XmlNsEvent)]
    public class WorldDisconnectEventArgsT<TWorld> : Abstract.BaseWorldDisconnectEventArgs<TWorld>
        where TWorld : class, IWorld, new()
    {}
    [Serializable]
    [XmlRoot("OnWorldList", Namespace = Global.XmlNsEvent)]
    public class WorldListEventArgsT<TWorld> : Abstract.BaseWorldListEventArgs<TWorld>
        where TWorld : class, IWorld, new()
    { }
    [Serializable]
    [XmlRoot("OnWorldSettingsChanged", Namespace = Global.XmlNsEvent)]
    public class WorldSettingsChangedEventArgsT<TWorld> : Abstract.BaseWorldSettingsChangedEventArgs<TWorld>
        where TWorld : class, IWorld, new()
    { }
    [Serializable]
    [XmlRoot("OnWorldEnter", Namespace = Global.XmlNsEvent)]
    public class WorldEnterEventArgsT<TWorld> : Abstract.BaseWorldEnterEventArgs<TWorld>
    where TWorld : class, IWorld, new() 
    { }
    [Serializable]
    [XmlRoot("OnWorldLeave", Namespace = Global.XmlNsEvent)]
    public class WorldLeaveEventArgsT<TWorld> : Abstract.BaseWorldLeaveEventArgs<TWorld>
    where TWorld : class, IWorld, new()
    { }
}
