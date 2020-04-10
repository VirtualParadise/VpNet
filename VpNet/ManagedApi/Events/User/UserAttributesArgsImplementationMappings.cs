using System.Xml.Serialization;

namespace VpNet
{
    [XmlRoot("OnUserAttributes", Namespace = Global.XmlNsEvent)]
    public class UserAttributesEventArgs : Abstract.BaseUserAttributesEventArgs
    { }
}
