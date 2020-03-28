using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("UserAttributes", Namespace = Global.XmlNs)]
    public class UserAttributes : Abstract.BaseUserAttributes
    {
    }
}
