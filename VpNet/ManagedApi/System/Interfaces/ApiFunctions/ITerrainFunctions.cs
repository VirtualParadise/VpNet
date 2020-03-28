namespace VpNet.Interfaces
{
    public interface ITerrainFunctions<in TTerrainTile,TTerrainNode,TTerrainCell>
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode,TTerrainCell>, new()
    {
        /// <summary>
        /// Queries a Terrain using value types for the query.
        /// </summary>
        /// <param name="tileX">The tile X.</param>
        /// <param name="tileZ">The tile Z.</param>
        /// <param name="revision">The revision.</param>
        /// <returns></returns>
        void TerrianQuery(int tileX, int tileZ, int[,] revision);
        /// <summary>
        /// Sets the terrain node.
        /// </summary>
        /// <param name="tileX">X position of the tile.</param>
        /// <param name="tileZ">Z position of the tile.</param>
        /// <param name="nodeX">X node of the tile.</param>
        /// <param name="nodeZ">Y node of the tile.</param>
        /// <param name="cells">The cells in the tile.</param>
        /// <returns></returns>
        void SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells);
    }
}
