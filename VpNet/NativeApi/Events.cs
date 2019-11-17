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

namespace VpNet.NativeApi
{
    /// <summary>
    /// Event Types
    /// </summary>
    internal enum Events
    {
        /// <summary>
        /// Chat event
        /// </summary>
        Chat = 0,
        /// <summary>
        /// Avatar add event
        /// </summary>
        AvatarAdd = 1,
        /// <summary>
        /// Avatar change event
        /// </summary>
        AvatarChange = 2,
        /// <summary>
        /// Avatar delete event
        /// </summary>
        AvatarDelete = 3,
        /// <summary>
        /// Object event
        /// </summary>
        Object = 4,
        /// <summary>
        /// Object change event
        /// </summary>
        ObjectChange = 5,
        /// <summary>
        /// Oject delete event
        /// </summary>
        ObjectDelete = 6,
        /// <summary>
        /// Object click event
        /// </summary>
        ObjectClick = 7,
        /// <summary>
        /// World list event
        /// </summary>
        WorldList = 8,
        /// <summary>
        /// World setting event
        /// </summary>
        WorldSetting = 9,
        /// <summary>
        /// World settings changed event
        /// </summary>
        WorldSettingsChanged = 10,
        /// <summary>
        /// Friend event
        /// </summary>
        Friend = 11,
        /// <summary>
        /// World disconnect event
        /// </summary>
        WorldDisconnect = 12,
        /// <summary>
        /// Universe disconnect event
        /// </summary>
        UniverseDisconnect = 13,
        /// <summary>
        /// User attributes event
        /// </summary>
        UserAttributes = 14,
        /// <summary>
        /// Cell query end event
        /// </summary>
        QueryCellEnd = 15,
        /// <summary>
        /// Terrain node event
        /// </summary>
        TerrainNode = 16,
        /// <summary>
        /// Avatar click event
        /// </summary>
        AvatarClick = 17,
        /// <summary>
        /// Teleport event
        /// </summary>
        Teleport = 18,
        /// <summary>
        /// Url Event
        /// </summary>
        Url = 19,
        /// <summary>
        /// The object bump begin
        /// </summary>
        ObjectBumpBegin,
        /// <summary>
        /// The object bump end
        /// </summary>
        ObjectBumpEnd,
        /// <summary>
        /// Terrain node changed
        /// </summary>
        TerrainNodeChanged,
        /// <summary>
        /// Join Event
        /// </summary>
        Join,
        /// <summary>
        /// Invite event
        /// </summary>
        Invite
    }
}
