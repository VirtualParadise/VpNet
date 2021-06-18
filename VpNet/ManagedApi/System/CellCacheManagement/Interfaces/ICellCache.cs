namespace VpNet.Interfaces
{
    interface CellCache
    {
        /// <summary>
        /// Adds the cell range to be cached specified by 2 cell (start to end cell)
        /// </summary>
        /// <param name="start">The starting cell (in no particular order).</param>
        /// <param name="end">The ending cell (in no particular order).</param>
        /// <returns>
        /// The number of cells that are in this range
        /// </returns>
        int AddCellRange(Cell start, Cell end);

        /// <summary>
        /// Adds a cell to be cached.
        /// </summary>
        /// <param name="cell">The cell.</param>
        void AddCell(Cell cell);
    }
}
