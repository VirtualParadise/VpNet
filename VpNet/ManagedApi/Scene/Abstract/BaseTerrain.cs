namespace VpNet.Abstract
{
    public abstract class BaseTerrain : ITerrain
    {
        public ushort Data { get; set; }
        public int NodeX { get; set; }
        public int NodeZ { get; set; }
        public int NodeRevision { get; set; }
        public int TileX { get; set; }
        public int TileZ { get; set; }
    }
}