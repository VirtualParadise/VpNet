using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("OnWorldDisconnect", Namespace = Global.XmlNsEvent)]
    public class WorldDisconnectEventArgs : Abstract.BaseWorldDisconnectEventArgs{}
    [Serializable]
    [XmlRoot("OnWorldList", Namespace = Global.XmlNsEvent)]
    public class WorldListEventArgs : Abstract.BaseWorldListEventArgs { }
    [Serializable]
    [XmlRoot("OnWorldSettingsChanged", Namespace = Global.XmlNsEvent)]
    public class WorldSettingsChangedEventArgs : Abstract.BaseWorldSettingsChangedEventArgs { }
    [Serializable]
    [XmlRoot("OnWorldEnter", Namespace = Global.XmlNsEvent)]
    public class WorldEnterEventArgs : Abstract.BaseWorldEnterEventArgs { }
    [XmlRoot("OnWorldLeave", Namespace = Global.XmlNsEvent)]
    public class WorldLeaveEventArgs : Abstract.BaseWorldLeaveEventArgs { }
}
