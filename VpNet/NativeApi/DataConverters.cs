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
