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
        protected BaseTerrainNode(IntPtr pointer)
        {
            X        = Functions.vp_int(pointer, Attributes.TerrainNodeX);
            Z        = Functions.vp_int(pointer, Attributes.TerrainNodeZ);
            Revision = Functions.vp_int(pointer, Attributes.TerrainNodeRevision);
            var data = Functions.GetData(pointer, Attributes.TerrainNodeData);
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