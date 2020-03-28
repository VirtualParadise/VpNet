using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [XmlRoot("OnUserAttributes", Namespace = Global.XmlNsEvent)]
    public class UserAttributesEventArgsT<TUserAttributes> : Abstract.BaseUserAttributesEventArgs<TUserAttributes> 
       where TUserAttributes : class, IUserAttributes,new()
    { }
}
