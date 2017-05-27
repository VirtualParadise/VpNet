#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2016 CUBE3 (Cit:36)
    Portions herein taken from: Roy Curtis (Cit:182)
    https://github.com/RoyCurtis/VPNet

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTerrainTile<TTerrainTile, TTerrainNode,TTerrainCell> : ITerrainTile<TTerrainTile, TTerrainNode,TTerrainCell>
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile, TTerrainNode, TTerrainCell>, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
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

        public TTerrainNode[,] Nodes = new TTerrainNode[4,4];
        public int X;
        public int Z;

        /// <summary>
        /// Gets or sets a TerrainNode object based on one-dimensional index, in column-major
        /// order (e.g. TerrainTile[4] = col 1, row 0)
        /// </summary>
        public TTerrainNode this[int i]
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
        public TTerrainNode this[int x, int z]
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