using System;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnQueryCellEnd" />. 
    /// </summary>
    [Serializable]
    public sealed class QueryCellEndArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="QueryCellEndArgs" /> class.
        /// </summary>
        /// <param name="cell">The cell which was queried.</param>
        public QueryCellEndArgs(Cell cell)
        {
            Cell = cell;
        }

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <value>The cell.</value>
        public Cell Cell { get; }
    }
}
