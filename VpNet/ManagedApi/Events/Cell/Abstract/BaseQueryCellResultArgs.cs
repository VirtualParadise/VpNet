using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class QueryCellResultArgs<TVpObject> : TimedEventArgs, IQueryCellResultArgs<TVpObject>
        where TVpObject : class, IVpObject, new()
    {
        public TVpObject VpObject { get; set; }

        protected QueryCellResultArgs(TVpObject vpObject)
        {
            VpObject = vpObject;
        }

        protected QueryCellResultArgs() { }
    }
}
