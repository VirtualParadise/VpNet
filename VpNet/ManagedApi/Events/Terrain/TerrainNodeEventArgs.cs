using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnTerrainNode" />.
    /// </summary>
    public class TerrainNodeEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TerrainNodeEventArgs" /> class.
        /// </summary>
        /// <param name="terrain">The terrain.</param>
        public TerrainNodeEventArgs(ITerrain terrain)
        {
            Terrain = terrain;
        }

        /// <summary>
        ///     Gets the terrain.
        /// </summary>
        /// <value>The terrain.</value>
        public ITerrain Terrain { get; }
    }
}
