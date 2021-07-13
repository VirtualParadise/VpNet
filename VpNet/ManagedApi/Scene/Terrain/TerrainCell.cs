namespace VpNet
{
    /// <summary>
    ///     Represents a terrain cell.
    /// </summary>
    public class TerrainCell
    {
        /// <summary>
        ///     Gets or sets the height of this terrain cell.
        /// </summary>
        /// <value>The height of this terrain cell.</value>
        public double Height { get; set; }
        
        /// <summary>
        ///     Gets or sets the attributes for this terrain cell. 
        /// </summary>
        /// <value>The attributes for this terrain cell.</value>
        /// <remarks>This value is a bit mask. See related entries for more useful interface with this mask.</remarks>
        /// <seealso cref="IsHole" />
        /// <seealso cref="Rotation" />
        /// <seealso cref="Texture" />
        public ushort Attributes { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this terrain cell is a hole.
        /// </summary>
        /// <value><see langword="true" /> if this cell is a hole; or <see langword="false" /> if this cell is filled.</value>
        public bool IsHole
        {
            get => (Attributes & 0x8000) >> 15 == 1;
            set => Attributes = (ushort) (Attributes | ((value ? 1 : 0) << 15));
        }

        /// <summary>
        ///     Gets or sets the rotation of this terrain cell.
        /// </summary>
        /// <value>
        ///     The rotation of this terrain cell, as represented by a compass direction provided by
        ///     <see cref="TerrainRotation" />.
        /// </value>
        public TerrainRotation Rotation
        {
            get => (TerrainRotation) ((Attributes & 0x6000) >> 13);
            set => Attributes = (ushort) (Attributes | ((int)value << 13));
        }
        
        /// <summary>
        ///     Gets or sets the texture of this terrain cell.
        /// </summary>
        /// <value>The texture of this terrain cell.</value>
        public ushort Texture
        {
            get => (ushort) (Attributes & 0x0FFF);
            set => Attributes = (ushort) (Attributes | (value & 0x1FFF));
        }
    }
}