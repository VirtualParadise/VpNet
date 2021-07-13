namespace VpNet
{
    public class TerrainTile 
    {
        /// <summary>
        /// A 2D array of revision numbers to force the server to send even unmodified
        /// terrain nodes back
        /// </summary>
        public static int[,] BaseRevision = new int[4, 4]
        {
            { -1, -1, -1, -1 },
            { -1, -1, -1, -1 },
            { -1, -1, -1, -1 },
            { -1, -1, -1, -1 }
        };

        public TerrainNode[,] Nodes = new TerrainNode[4,4];
        public int X;
        public int Z;

        /// <summary>
        /// Gets or sets a TerrainNode object based on one-dimensional index, in column-major
        /// order (e.g. TerrainTile[4] = col 1, row 0)
        /// </summary>
        public TerrainNode this[int i]
        {
            get
            {
                var x = i % 4;
                var z = (i - x) / 4;
                return this[x, z];
            }

            set
            {
                var x = i % 4;
                var z = (i - x) / 4;
                this[x, z] = value;
            }
        }

        /// <summary>
        /// Gets or sets a TerrainNode object based on two-dimensional index.
        /// Automatically sets the node's X, Y and Parent value
        /// </summary>
        public TerrainNode this[int x, int z]
        {
            get { return Nodes[x, z]; }
            set {
                value.X = x;
                value.Z = z;
                value.Parent = this;
                Nodes[x, z] = value;
            }
        }
    }
}