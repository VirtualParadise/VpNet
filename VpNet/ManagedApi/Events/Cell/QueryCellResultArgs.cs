using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [Serializable]
    [XmlRoot("OnQueryCellResult", Namespace = Global.XmlNsEvent)]
    public partial class QueryCellResultArgs : Abstract.QueryCellResultArgs
    {
        public QueryCellResultArgs(VpObject vpObject) : base(vpObject)
        {
        }

        public QueryCellResultArgs() { }
    }
}
