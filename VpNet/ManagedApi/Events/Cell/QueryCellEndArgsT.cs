using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [Serializable]
    [XmlRoot("OnQueryCellEnd", Namespace = Global.XmlNsEvent)]
    public class QueryCellEndArgsT<TCell> : Abstract.BaseQueryCellEndArgs
        {
    }
}
