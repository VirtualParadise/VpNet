namespace VpNet.Interfaces
{
    /// <summary>
    ///  Query Cell End event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TCell">The type of the cell.</typeparam>
    public interface IQueryCellEndArgs
    {
        /// <summary>
        /// Gets or sets the cell.
        /// </summary>
        /// <value>
        /// The cell.
        /// </value>
        ICell Cell { get; set; }
    }
}