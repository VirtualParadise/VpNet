using VpNet.Abstract;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Terrain Node event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TTerrain">The type of the terrain.</typeparam>
    public interface ITerrainNodeEventArgs<TTerrain> where TTerrain : class, ITerrain, new()
    {
        /// <summary>
        /// Gets or sets the terrain.
        /// </summary>
        /// <value>
        /// The terrain.
        /// </value>
        TTerrain Terrain { get; set; }
    }
}