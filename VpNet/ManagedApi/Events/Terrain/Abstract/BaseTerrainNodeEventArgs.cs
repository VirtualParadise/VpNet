using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public class BaseTerrainNodeEventArgs<TTerrain> : TimedEventArgs, ITerrainNodeEventArgs<TTerrain> where TTerrain : class,  ITerrain, new()
    {
        public TTerrain Terrain { get; set; }

        public BaseTerrainNodeEventArgs(TTerrain terrain)
        {
            Terrain = terrain;
        } 

        public BaseTerrainNodeEventArgs(){} 
    }
}
