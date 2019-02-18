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
    /// <summary>
    /// Teleport event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleportEventArgs<TTeleport, TWorld, TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar,  new()
        where TTeleport : class, ITeleport<TWorld, TAvatar>, new()
    {
        /// <summary>
        /// Gets or sets the teleport.
        /// </summary>
        /// <value>
        /// The teleport.
        /// </value>
        TTeleport Teleport { get; set; }
    }
}
