using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseQueryCellEndArgs<TCell> : TimedEventArgs, IQueryCellEndArgs<TCell> where TCell : class, ICell, new()
    {
        public TCell Cell { get; set; }

        protected BaseQueryCellEndArgs(TCell cell)
        {
            Cell = cell;
        }

        protected BaseQueryCellEndArgs() { }
    }
}
