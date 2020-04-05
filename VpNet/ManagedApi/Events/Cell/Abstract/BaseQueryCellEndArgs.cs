using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseQueryCellEndArgs : TimedEventArgs, IQueryCellEndArgs
    {
        public ICell Cell { get; set; }

        protected BaseQueryCellEndArgs(ICell cell)
        {
            Cell = cell;
        }

        protected BaseQueryCellEndArgs() { }
    }
}
