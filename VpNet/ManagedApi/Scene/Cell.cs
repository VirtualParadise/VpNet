namespace VpNet
{
    /// <summary>
    ///     Represents a cell.
    /// </summary>
    public struct Cell
    {
        /// <summary>
        ///     The center cell of the world (0, 0).
        /// </summary>
        public static readonly Cell Zero = new Cell(0, 0);
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="Cell" /> struct.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="z">The Z coordinate of the cell.</param>
        public Cell(int x, int z)
        {
            X = x;
            Z = z;
        }
        
        /// <summary>
        ///     Gets or sets the X coordinate of the cell.
        /// </summary>
        /// <value>The X coordinate of the cell.</value>
        public int X { get; set;}
        
        /// <summary>
        ///     Gets or sets the Z coordinate of the cell.
        /// </summary>
        /// <value>The Z coordinate of the cell.</value>
        public int Z { get; set; }
    }
}
