namespace VpNet.Interfaces
{
    public interface ITerrainTile<TTerrainTile, TTerrainNode,TTerrainCell>
        where TTerrainCell : class, ITerrainCell ,new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode,TTerrainCell>,new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
    {
        /// <summary>
        /// Gets or sets a TerrainNode object based on one-dimensional index, in column-major
        /// order (e.g. TerrainTile[4] = col 1, row 0)
        /// </summary>
        TTerrainNode this[int i] { get; set; }

        /// <summary>
        /// Gets or sets a TerrainNode object based on two-dimensional index.
        /// Automatically sets the node's X, Y and Parent value
        /// </summary>
        TTerrainNode this[int x, int z] { get; set; }
    }
}