using System;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnQueryCellResult" />.
    /// </summary>
    [Serializable]
    [XmlRoot("OnQueryCellResult", Namespace = Global.XmlNsEvent)]
    public sealed class QueryCellResultArgs : TimedEventArgs
    {
        public QueryCellResultArgs(VpObject vpObject)
        {
            Object = vpObject;
        }
        
        /// <summary>
        ///     Gets the object returned by the query.
        /// </summary>
        /// <value>The object returned by the query.</value>
        public VpObject Object { get; set; }
    }
}
