using System;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnQueryCellResult" />.
    /// </summary>
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
