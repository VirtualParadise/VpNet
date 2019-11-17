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

using System;

namespace VpNet.NativeApi
{
    /// <summary>
    /// Attribute type
    /// </summary>
    internal enum IntegerAttribute
    {
        /// <summary>
        /// Avatar session
        /// </summary>
        AvatarSession = 0,
        /// <summary>
        /// Avatar type
        /// </summary>
        AvatarType,
        /// <summary>
        /// My type
        /// </summary>
        MyType,
        /// <summary>
        /// Object Id
        /// </summary>f
        ObjectId,
        /// <summary>
        /// Object Type
        /// </summary>
        ObjectType,
        /// <summary>
        /// Object Time
        /// </summary>
        ObjectTime,
        /// <summary>
        /// Object User Id
        /// </summary>
        ObjectUserId,
        /// <summary>
        /// World State
        /// </summary>
        WorldState,
        /// <summary>
        /// World Users
        /// </summary>
        WorldUsers,
        /// <summary>
        /// Reference number
        /// </summary>
        ReferenceNumber,
        /// <summary>
        /// Callback
        /// </summary>
        Callback,
        /// <summary>
        /// User id
        /// </summary>
        UserId,
        /// <summary>
        /// User registration time
        /// </summary>
        UserRegistrationTime,
        /// <summary>
        /// User online time
        /// </summary>
        UserOnlineTime,
        /// <summary>
        /// User last login
        /// </summary>
        UserLastLogin,
        /// <summary>
        /// Friend id
        /// </summary>
        [Obsolete]
        FriendId,
        /// <summary>
        /// Friend user id
        /// </summary>
        [Obsolete]
        FriendUserId,
        /// <summary>
        /// Friend online
        /// </summary>
        [Obsolete]
        FriendOnline,
        /// <summary>
        /// My user id
        /// </summary>
        MyUserId,
        /// <summary>
        /// Proxy server type
        /// </summary>
        ProxyType,
        /// <summary>
        /// Proxy server port
        /// </summary>
        ProxyPort,
        /// <summary>
        /// Cell X
        /// </summary>
        CellX,
        /// <summary>
        /// Cell Z
        /// </summary>
        CellZ,
        /// <summary>
        /// Terrain tile X
        /// </summary>
        TerrainTileX,
        /// <summary>
        /// Terrain tile Z
        /// </summary>
        TerrainTileZ,
        /// <summary>
        /// Terrain node X
        /// </summary>
        TerrainNodeX,
        /// <summary>
        /// Terrain node Z
        /// </summary>
        TerrainNodeZ,
        /// <summary>
        /// Terrain node revision
        /// </summary>
        TerrainNodeRevision,
        /// <summary>
        /// Clicked Session
        /// </summary>
        ClickedSession,
        /// <summary>
        /// Chat Type
        /// </summary>
        ChatType,
        /// <summary>
        /// Chat Color Red
        /// </summary>
        ChatRolorRed,
        /// <summary>
        /// Chat Color Green
        /// </summary>
        ChatColorGreen,
        /// <summary>
        /// Chat Color Blue
        /// </summary>
        ChatColorBlue,
        /// <summary>
        /// Chat Effects
        /// </summary>
        ChatEffects,
        /// <summary>
        /// Disconnect Error Code
        /// </summary>
        DisconnectErrorCode,
        /// <summary>
        /// Url Target
        /// </summary>
        UrlTarget,
        /// <summary>
        /// The current event
        /// </summary>
        CurrentEvent,
        /// <summary>
        /// The current callback
        /// </summary>
        CurrentCallback,
        /// <summary>
        /// The cell revision
        /// </summary>
        CellRevision,
        /// <summary>
        /// The cell status
        /// </summary>
        CellStatus,
        /// <summary>
        /// The join identifier
        /// </summary>
        JoinId,
        /// <summary>
        /// The invitation identifier
        /// </summary>
        InviteId,
        /// <summary>
        /// User ID of the user who send the invitation
        /// </summary>
        InviteUserId
    }
}
