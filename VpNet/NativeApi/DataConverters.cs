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
    30-05-2013 cube3: added Generics

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
using System.IO;
using System.Runtime.InteropServices;
using VpNet.Interfaces;

namespace VpNet.NativeApi
{
    internal static class DataConverters
    {
        /// <summary>
        /// Converts terrain node data to a 2D TerrainCell array
        /// </summary>
        public static TTerrainCell[,] NodeDataTo2DArray<TTerrainCell>(byte[] data)
            where TTerrainCell : class, ITerrainCell, new()
        {
            var cells = new TTerrainCell[8, 8];

            using (var memStream = new MemoryStream(data))
            {
                var array = new byte[8];
                var pin = GCHandle.Alloc(array, GCHandleType.Pinned);
                for (var i = 0; i < 64; i++)
                {
                    if (memStream.Read(array, 0, 8) < 8)
                        throw new Exception("Unexpected end of byte array");
                    var cell = (TTerrainCell)Marshal.PtrToStructure(pin.AddrOfPinnedObject(), typeof(TTerrainCell));


                    var x = i % 8;
                    var z = (i - x) / 8;
                    cells[x, z] = cell;
                }
                pin.Free();
            }
            return cells;
        }

        /// <summary>
        /// Converts a 2D TerrainCell array to raw VP terrain data
        /// </summary>
        /// <remarks>http://stackoverflow.com/a/650886</remarks>
        internal static byte[] NodeToNodeData(TerrainNode node)
        {
            var data = new byte[512];

            for (var i = 0; i < 64; i++)
            {
                var cell = node[i];
                var buffer = Marshal.AllocHGlobal(8);
                var array = new byte[8];
                Marshal.StructureToPtr(cell, buffer, false);
                Marshal.Copy(buffer, data, i * 8, 8);
                Marshal.FreeHGlobal(buffer);
            }

            return data;
        }


    }
}
