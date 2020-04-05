using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public class BaseTerrainNodeEventArgs : TimedEventArgs, ITerrainNodeEventArgs
    {
        public ITerrain Terrain { get; set; }

        public BaseTerrainNodeEventArgs(ITerrain terrain)
        {
            Terrain = terrain;
        } 

        public BaseTerrainNodeEventArgs(){} 
    }
}
