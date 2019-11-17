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
    internal enum FloatAttribute
    {
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
        InvitePitch
    }
}
