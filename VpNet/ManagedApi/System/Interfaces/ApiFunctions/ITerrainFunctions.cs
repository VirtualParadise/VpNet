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

namespace VpNet.Interfaces
{
    public interface ITerrainFunctions<in TTerrainTile,TTerrainNode,TTerrainCell>
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode,TTerrainCell>, new()
    {
        /// <summary>
        /// Queries a Terrain using value types for the query.
        /// </summary>
        /// <param name="tileX">The tile X.</param>
        /// <param name="tileZ">The tile Z.</param>
        /// <param name="revision">The revision.</param>
        /// <returns></returns>
        void TerrianQuery(int tileX, int tileZ, int[,] revision);
        /// <summary>
        /// Sets the terrain node.
        /// </summary>
        /// <param name="tileX">X position of the tile.</param>
        /// <param name="tileZ">Z position of the tile.</param>
        /// <param name="nodeX">X node of the tile.</param>
        /// <param name="nodeZ">Y node of the tile.</param>
        /// <param name="cells">The cells in the tile.</param>
        /// <returns></returns>
        void SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells);
    }
}
