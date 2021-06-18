using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseQueryCellEndArgs : TimedEventArgs, IQueryCellEndArgs
    {
        public Cell Cell { get; set; }

        protected BaseQueryCellEndArgs(Cell cell)
        {
            Cell = cell;
        }

        protected BaseQueryCellEndArgs() { }
    }
}
