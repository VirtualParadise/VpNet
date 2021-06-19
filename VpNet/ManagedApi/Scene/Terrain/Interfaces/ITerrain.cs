namespace VpNet.Abstract
{
    public interface ITerrain
    {
        ushort Data { get; set; }
        int NodeX { get; set; }
        int NodeZ { get; set; }
        int NodeRevision { get; set; }
        int TileX { get; set; }
        int TileZ { get; set; }
    }
}