using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectChange", Namespace = Global.XmlNsEvent)]
    public class ObjectChangeArgs : Abstract.BaseObjectChangeArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectClick", Namespace = Global.XmlNsEvent)]
    public class ObjectClickArgs : Abstract.BaseObjectClickArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectCreate", Namespace = Global.XmlNsEvent)]
    public class ObjectCreateArgs : Abstract.BaseObjectCreateArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectDelete", Namespace = Global.XmlNsEvent)]
    public class ObjectDeleteArgs : Abstract.BaseObjectDeleteArgs { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectBump", Namespace = Global.XmlNsEvent)]
    public class ObjectBumpArgs : Abstract.BaseObjectBumpArgs { }
}
