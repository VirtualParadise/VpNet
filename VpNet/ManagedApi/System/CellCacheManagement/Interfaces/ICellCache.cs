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
    interface ICellCache<TVpObject, in TCell>
        where TVpObject : class, IVpObject, new()
        where TCell : class, ICell,new()
    {
        /// <summary>
        /// Adds the cell range to be cached specified by 2 cell (start to end cell)
        /// </summary>
        /// <param name="start">The starting cell (in no particular order).</param>
        /// <param name="end">The ending cell (in no particular order).</param>
        /// <returns>
        /// The number of cells that are in this range
        /// </returns>
        int AddCellRange(TCell start, TCell end);

        /// <summary>
        /// Adds a cell to be cached.
        /// </summary>
        /// <param name="cell">The cell.</param>
        void AddCell(TCell cell);
    }
}
