using System;
using VpNet.Interfaces;
using VpNet.NativeApi;

namespace VpNet.Abstract
{
    public abstract class BaseTerrainNode<TTerrainTile, TTerrainNode, TTerrainCell> : ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode, TTerrainCell>, new()
    {
        public ITerrainTile<TTerrainTile, TTerrainNode, TTerrainCell> Parent { get; set; }
        public TTerrainCell[,] Cells { get; set; }
        public int X { get; set; }
        public int Z { get; set; }
        public int Revision { get; set; }

        protected BaseTerrainNode()
        {
           Cells = new TTerrainCell[8,8];
        }

        /// <summary>
        /// Creates a terrain node from an instances' attributes and byte array
        /// </summary>
        protected BaseTerrainNode(IntPtr instanceHandle)
        {
            X = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeX);
            Z = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeZ);
            Revision = Functions.vp_int(instanceHandle, IntegerAttribute.TerrainNodeRevision);
            var data = Functions.GetData(instanceHandle, DataAttribute.TerrainNodeData);
            Cells    = DataConverters.NodeDataTo2DArray<TTerrainCell>(data);
        }

        /// <summary>
        /// Gets or sets a TerrainCell value based on one-dimensional index, in X-major
        /// order (e.g. TerrainNode[5] = col 5, row 0 or X5 Z0)
        /// </summary>
        public TTerrainCell this[int i]
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
        /// Gets or sets a TerrainCell value based on two-dimensional index
        /// </summary>
        public TTerrainCell this[int x, int z]
        {
            get { return Cells[x, z]; }
            set { Cells[x, z] = value; }
        }
    }
}