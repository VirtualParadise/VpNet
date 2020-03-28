using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("OnWorldDisconnect", Namespace = Global.XmlNsEvent)]
    public class WorldDisconnectEventArgs : Abstract.BaseWorldDisconnectEventArgs<World>{}
    [Serializable]
    [XmlRoot("OnWorldList", Namespace = Global.XmlNsEvent)]
    public class WorldListEventArgs : Abstract.BaseWorldListEventArgs<World> { }
    [Serializable]
    [XmlRoot("OnWorldSettingsChanged", Namespace = Global.XmlNsEvent)]
    public class WorldSettingsChangedEventArgs : Abstract.BaseWorldSettingsChangedEventArgs<World> { }
    [Serializable]
    [XmlRoot("OnWorldEnter", Namespace = Global.XmlNsEvent)]
    public class WorldEnterEventArgs : Abstract.BaseWorldEnterEventArgs<World> { }
    [XmlRoot("OnWorldLeave", Namespace = Global.XmlNsEvent)]
    public class WorldLeaveEventArgs : Abstract.BaseWorldLeaveEventArgs<World> { }
}
