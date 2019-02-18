#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET.

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
    /// <summary>
    /// Avatar Teleport Functions
    /// </summary>
    /// <typeparam name="TRc">The type of the rc.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleportFunctions<out TRc, in TWorld, in TAvatar>
        where TRc : class, IRc, new()
        where TAvatar: class, IAvatar, new()
    {
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar, string world, double x, double y, double z, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar, string world, Vector3 position, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="targetSession">The target session.</param>
        /// <param name="world">The world.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        TRc TeleportAvatar(int targetSession, string world, double x, double y, double z, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="targetSession">The target session.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        TRc TeleportAvatar(int targetSession, string world, Vector3 position, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar, string world, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar, TWorld world, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar within the current world.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar within the current world b previously having changed the avatar position and rotation properties
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <returns></returns>
        TRc TeleportAvatar(TAvatar avatar);
    }
}
