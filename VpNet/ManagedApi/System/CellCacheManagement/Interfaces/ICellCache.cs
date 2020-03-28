namespace VpNet.Interfaces
{
    interface ICellCache<TVpObject, in TCell>
        where TVpObject : class, IVpObject, new()
        where TCell : class, ICell,new()
    {
        /// <summary>
        /// Adds the cell range to be cached specified by 2 cell (start to end cell)
        /// </summary>
        /// <param name="start">The starting cell (in no particular order).</param>
        /// <param name="end">The ending cell (in no particular order).</param>
        /// <returns>
        /// The number of cells that are in this range
        /// </returns>
        int AddCellRange(TCell start, TCell end);

        /// <summary>
        /// Adds a cell to be cached.
        /// </summary>
        /// <param name="cell">The cell.</param>
        void AddCell(TCell cell);
    }
}
