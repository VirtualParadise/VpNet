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
    internal enum Attributes
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
        InviteUserId,
        /// <summary>
        /// Integer attribute highest
        /// </summary>
        IntegerAttributeHighest,
        /// <summary>
        /// Avatar x
        /// </summary>
        AvatarX = 0,
        /// <summary>
        /// Avatar y
        /// </summary>
        AvatarY,
        /// <summary>
        /// Avatar z
        /// </summary>
        AvatarZ,
        /// <summary>
        /// Avatar yaw
        /// </summary>
        AvatarYaw,
        /// <summary>
        /// Avatar pitch
        /// </summary>
        AvatarPitch,
        /// <summary>
        /// My x
        /// </summary>
        MyX,
        /// <summary>
        /// My y
        /// </summary>
        MyY,
        /// <summary>
        /// My z
        /// </summary>
        MyZ,
        /// <summary>
        /// My yaw
        /// </summary>
        MyYaw,
        /// <summary>
        /// My Pitch
        /// </summary>
        MyPitch,
        /// <summary>
        /// Object x
        /// </summary>
        ObjectX,
        /// <summary>
        /// Object y
        /// </summary>
        ObjectY,
        /// <summary>
        /// Object z
        /// </summary>
        ObjectZ,
        /// <summary>
        /// Object rotation x
        /// </summary>
        ObjectRotationX,
        /// <summary>
        /// Object rotation y
        /// </summary>
        ObjectRotationY,
        /// <summary>
        /// Object rotation z
        /// </summary>
        ObjectRotationZ,
        /// <summary>
        /// Object yaw
        /// </summary>
        ObjectYaw = ObjectRotationX,
        /// <summary>
        /// Object pitch
        /// </summary>
        ObjectPitch = ObjectRotationY,
        /// <summary>
        /// Object roll
        /// </summary>
        ObjectRoll = ObjectRotationZ,
        /// <summary>
        /// Object rotation angle
        /// </summary>
        ObjectRotationAngle,
        /// <summary>
        /// Teleport X
        /// </summary>
        TeleportX,
        /// <summary>
        /// Teleport Y
        /// </summary>
        TeleportY,
        /// <summary>
        /// Teleport Z
        /// </summary>
        TeleportZ,
        /// <summary>
        /// Teleport Yaw
        /// </summary>
        TeleportYaw,
        /// <summary>
        /// Teleport Pitch
        /// </summary>
        TeleportPitch,
        /// <summary>
        /// Click Hit X
        /// </summary>
        ClickHitX,
        /// <summary>
        /// Click Hit Y
        /// </summary>
        ClickHitY,
        /// <summary>
        /// click Hit Z
        /// </summary>
        ClickHitZ,
        /// <summary>
        /// The join x
        /// </summary>
        JoinX,
        /// <summary>
        /// The join y
        /// </summary>
        JoinY,
        /// <summary>
        /// The join z
        /// </summary>
        JoinZ,
        /// <summary>
        /// The join yaw
        /// </summary>
        JoinYaw,
        /// <summary>
        /// The join pitch
        /// </summary>
        JoinPitch,
        /// <summary>
        /// Invitation X position
        /// </summary>
        InviteX,
        /// <summary>
        /// Invitation Y position
        /// </summary>
        InviteY,
        /// <summary>
        /// Invitation Z position
        /// </summary>
        InviteZ,
        /// <summary>
        /// Invitation yaw
        /// </summary>
        InviteYaw,
        /// <summary>
        /// Invitation pitch
        /// </summary>
        InvitePitch,
        /// <summary>
        /// double attribute highest
        /// </summary>
        doubleAttributeHighest,
        /// <summary>
        /// Avatar name
        /// </summary>
        AvatarName = 0,
        /// <summary>
        /// Chat message
        /// </summary>
	    ChatMessage,
        /// <summary>
        /// Object model
        /// </summary>
	    ObjectModel,
        /// <summary>
        /// Object action
        /// </summary>
	    ObjectAction,
        /// <summary>
        /// Object description
        /// </summary>
	    ObjectDescription,
        /// <summary>
        /// World name
        /// </summary>
	    WorldName,
        /// <summary>
        /// User name
        /// </summary>
	    UserName,
        /// <summary>
        /// User email
        /// </summary>
	    UserEmail,
        /// <summary>
        /// World settings key
        /// </summary>
	    WorldSettingKey,
        /// <summary>
        /// World settings value
        /// </summary>
	    WorldSettingValue,
        /// <summary>
        /// Friend name
        /// </summary>
	    FriendName,
        /// <summary>
        /// The proxy host voor universe connections.
        /// </summary>
        ProxyHost,
        /// <summary>
        /// The world the avatar was teleported to.
        /// </summary>
        TeleportWorld,
        /// <summary>
        /// Url
        /// </summary>
        Url,
        /// <summary>
        /// The join world
        /// </summary>
        JoinWorld,
        /// <summary>
        /// The join name
        /// </summary>
        JoinName,
        /// <summary>
        /// The start world
        /// </summary>
        StartWorld,
        /// <summary>
        /// Name of the user who sent the invitation
        /// </summary>
        InviteName,
        /// <summary>
        /// Name of the world we're being invited to
        /// </summary>
        InviteWorld,
        /// <summary>
        /// Name of the connecting application
        /// </summary>
        ApplicationName,
        /// <summary>
        /// Version of the connecting application
        /// </summary>
        ApplicationVersion,
        /// <summary>
        /// Name of the application used by this avatar
        /// </summary>
        AvatarApplicationName,
        /// <summary>
        /// Version of the application used by this avatar
        /// </summary>
        AvatarApplicationVersion,
        /// <summary>
        /// String attribute highest
        /// </summary>
	    StringAttributeHighest,
        /// <summary>
        /// Object data
        /// </summary>
	    ObjectData = 0,
        /// <summary>
        /// The terrain node data
        /// </summary>
        TerrainNodeData = 1,
        /// <summary>
        /// VP highest data
        /// </summary>
	    HighestData,
        /// <summary>
        /// Connection uses no proxy.
        /// </summary>
        ProxyNone = 0,
        /// <summary>
        /// Connection uses socks 4A
        /// </summary>
        ProxySocks4A,
        /// <summary>
        /// VP Highest proxy type
        /// </summary>
        HighestProxyType,
        /// <summary>
        /// Open url in external browser
        /// </summary>
        UrlTargetBrowser = 0,
        /// <summary>
        /// Open url in internal browser as 2D overlay on top of the 3D world view
        /// </summary>
        UrlTargetOverlay,
        /// <summary>
        /// VP Highest Url Target
        /// </summary>
        HighestUrlTarget,
    }
}
