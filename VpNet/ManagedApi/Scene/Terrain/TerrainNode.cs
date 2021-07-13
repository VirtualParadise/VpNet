using System;
using VpNet.NativeApi;

namespace VpNet
{
    public class TerrainNode
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TerrainNode" /> class.
        /// </summary>
        public TerrainNode()
        {
            Cells = new TerrainCell[8, 8];
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TerrainNode" /> class.
        /// </summary>
        /// <param name="instanceHandle">The native instance handle from which to pull data.</param>
        public TerrainNode(IntPtr instanceHandle)
        {
            X = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeX);
            Z = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeZ);
            Revision = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeRevision);

            var data = Functions.GetData(instanceHandle, DataAttribute.TerrainNodeData);
            Cells = DataConverters.NodeDataTo2DArray(data);
        }

        /// <summary>
        ///     Gets or sets the cells within this node.
        /// </summary>
        public TerrainCell[,] Cells { get; set; }

        /// <summary>
        ///     Gets or sets the parent tile.
        /// </summary>
        /// <value>The parent tile.</value>
        public TerrainTile Parent { get; set; }

        /// <summary>
        ///     Gets or sets the tile revision number.
        /// </summary>
        /// <value>The tile revision number.</value>
        public int Revision { get; set; }

        public int X { get; set; }

        public int Z { get; set; }

        /// <summary>
        ///     Gets or sets the terrain cell value based on one-dimensional index, in X-major order (e.g. TerrainNode[5] = col 5,
        ///     row 0 or X5 Z0)
        /// </summary>
        /// <param name="i">The one-dimensional index by which to access <see cref="Cells" />.</param>
        /// <value>The cell at the specified index.</value>
        public TerrainCell this[int i]
        {
            get
            {
                var x = i % 8;
                var z = (i - x) / 8;
                return this[x, z];
            }
            set
            {
                var x = i % 8;
                var z = (i - x) / 8;
                this[x, z] = value;
            }
        }

        /// <summary>
        ///     Gets or sets a terrain cell value based on X and Z coordinates of the <see cref="Cells" /> array.
        /// </summary>
        /// <param name="x">The first-dimensional index by which to access <see cref="Cells" />.</param>
        /// <param name="z">The second-dimensional index by which to access <see cref="Cells" />.</param>
        /// <value>The cell at the specified index.</value>
        public TerrainCell this[int x, int z]
        {
            get => Cells[x, z];
            set => Cells[x, z] = value;
        }
    }
}
