using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTerrainCell : ITerrainCell
    {
        public double Height { get; set; }
        public ushort Attributes { get; set; }

        public bool IsHole
        {
            get { return (Attributes & 0x8000) >> 15 == 1; }
            set { Attributes = (ushort) (Attributes | ((value ? 1 : 0) << 15)); }
        }

        public TerrainRotation Rotation
        {
            get { return (TerrainRotation) ((Attributes & 0x6000) >> 13); }
            set { Attributes = (ushort) (Attributes | ((int)value << 13)); }
        }
        public ushort Texture
        {
            get { return (ushort) (Attributes & 0x0FFF);}
            set { Attributes = (ushort) (Attributes | (value & 0x1FFF)); }
        }
    }
}
