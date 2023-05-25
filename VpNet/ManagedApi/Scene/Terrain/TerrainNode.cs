using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using VpNet.NativeApi;

namespace VpNet
{
    public class TerrainNode
    {
		private const int TerrainCellDataSize = 8;
        private const int NodeDataSize = TerrainCellDataSize * 8 * 8;

		/// <summary>
		///     Initializes a new instance of the <see cref="TerrainNode" /> class.
		/// </summary>
		public TerrainNode()
        {
            Cells = new TerrainCell[8 * 8];
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

            var dataPtr = Functions.vp_data(instanceHandle, DataAttribute.TerrainNodeData, out int dataLength);
            if (dataLength != NodeDataSize)
            {
                throw new ArgumentException("Unexpected data size for terrain node data");
            }

            var cells = new TerrainCell[8 * 8];
            for (int i = 0; i < cells.Length; i++)
            {
				var cell = Marshal.PtrToStructure<TerrainCell>(dataPtr + TerrainCellDataSize * i);
				cells[i] = cell;
            }

            Cells = cells;
        }

        /// <summary>
        ///     Gets or sets the cells within this node.
        /// </summary>
        public TerrainCell[] Cells { get; set; }

        /// <summary>
        ///     Gets or sets the tile revision number.
        /// </summary>
        /// <value>The tile revision number.</value>
        public int Revision { get; set; }

        /// <summary>
        /// X-coordinate of node within a tile. 0-4.
        /// </summary>
        public int X { get; set; }

		/// <summary>
		/// Z-coordinate of node within a tile. 0-4.
		/// </summary>
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
                return Cells[i];
            }
            set
            {
                Cells[i] = value;
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
            get => Cells[z * 8 + x];
            set => Cells[z * 8 + x] = value;
        }
    }
}
