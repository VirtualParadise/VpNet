using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectChange",Namespace = Global.XmlNsEvent)]
    public class ObjectChangeArgsT<TAvatar, TVpObject> : Abstract.BaseObjectChangeArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectClick", Namespace = Global.XmlNsEvent)]
    public class ObjectClickArgsT<TAvatar, TVpObject> : Abstract.BaseObjectClickArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectCreate", Namespace = Global.XmlNsEvent)]
    public class ObjectCreateArgsT<TAvatar, TVpObject> : Abstract.BaseObjectCreateArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectDelete", Namespace = Global.XmlNsEvent)]
    public class ObjectDeleteArgsT<TAvatar, TVpObject> : Abstract.BaseObjectDeleteArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectBump", Namespace = Global.XmlNsEvent)]
    public class ObjectBumpArgsT<TAvatar, TVpObject> : Abstract.BaseObjectBumpArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new() { }
}
