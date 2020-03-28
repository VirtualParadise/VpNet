namespace VpNet.Interfaces
{
    /// <summary>
    /// Query Cell result event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    public interface IQueryCellResultArgs<TVpObject>
        where TVpObject : class, IVpObject, new()
    {
        /// <summary>
        /// Gets or sets the vp object.
        /// </summary>
        /// <value>
        /// The vp object.
        /// </value>
        TVpObject VpObject { get; set; }
    }
}