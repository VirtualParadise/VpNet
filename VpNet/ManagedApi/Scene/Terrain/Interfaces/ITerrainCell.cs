namespace VpNet.Interfaces
{
    public interface ITerrainCell
    {
        double Height { get; set; }

        ushort Attributes { get; set; }

        bool IsHole { get; set; }

        TerrainRotation Rotation { get; set; }

        ushort Texture { get; set; }
    }
}