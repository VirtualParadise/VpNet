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

using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IUniverseFunctions<TResult>
        where TResult : class, IRc, new()
    {
        Task<TResult> Connect(string host = "universe.virtualparadise.org", ushort port = 57000);
        Task<TResult> Login(string username, string password, string botname);
        /// <summary>
        /// Logs in the user, using the preloaded instance configuration.
        /// </summary>
        /// <returns></returns>
        Task<TResult> Login();
        /// <summary>
        /// Logs in to the universe and automatically enters the world using the preloaded instance configiguration.
        /// </summary>
        /// <param name="isAnnounceAvatar">if set to <c>true</c> [is announce avatar] then the avatar is updated on the given position as specified within the instance configuration. If the position is not specified, the avatar will appear at Ground Zero.</param>
        /// <returns></returns>
        Task<TResult> LoginAndEnter(bool isAnnounceAvatar = true);
    }
}
