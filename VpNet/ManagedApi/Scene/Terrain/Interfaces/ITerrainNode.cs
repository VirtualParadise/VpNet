namespace VpNet.Interfaces
{
    public interface ITerrainNode<TTerrainTile, TTerrainNode, TTerrainCell>
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile, TTerrainNode, TTerrainCell>, new()
    {
        /// <summary>
        /// Gets or sets a TerrainCell value based on one-dimensional index, in column-major
        /// order (e.g. TerrainNode[5] = col 0, row 5)
        /// </summary>
        TTerrainCell this[int i] { get; set; }

        /// <summary>
        /// Gets or sets a TerrainCell value based on two-dimensional index
        /// </summary>
        TTerrainCell this[int x, int z] { get; set; }

        int X { get; set; }
        int Z { get; set; }

         ITerrainTile<TTerrainTile,TTerrainNode,TTerrainCell> Parent { get; set; }
    }
}