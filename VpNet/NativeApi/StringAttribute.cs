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
    internal enum StringAttribute
    {
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
        AvatarApplicationVersion
    }
}
