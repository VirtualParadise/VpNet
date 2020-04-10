using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public class QueryCellResultArgs : TimedEventArgs, IQueryCellResultArgs
    {
        public IVpObject VpObject { get; set; }

        public QueryCellResultArgs(IVpObject vpObject)
        {
            VpObject = vpObject;
        }

        public QueryCellResultArgs() { }
    }
}
