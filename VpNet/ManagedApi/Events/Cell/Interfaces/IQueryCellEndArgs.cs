namespace VpNet.Interfaces
{
    /// <summary>
    ///  Query Cell End event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TCell">The type of the cell.</typeparam>
    public interface IQueryCellEndArgs<TCell> where TCell : class, ICell, new()
    {
        /// <summary>
        /// Gets or sets the cell.
        /// </summary>
        /// <value>
        /// The cell.
        /// </value>
        TCell Cell { get; set; }
    }
}