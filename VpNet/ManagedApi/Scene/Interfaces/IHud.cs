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
    /// Delegate for avatar hud element click.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    /// <param name="elementId">The element identifier.</param>
    public delegate void OnClickHudElement<TAvatar, TVector3>(TAvatar avatar, string elementId)
    where TVector3 : struct, IVector3
    where TAvatar : class, IAvatar<TVector3>, new();

    /// <summary>
    /// Minimal generic hud implementation interface. This interface is implemented by VPNet PLugins tht provide a certain rendering strategy.
    /// The first rendering strategy is based on JQuery. But since this is completely pluggable both on the server and client side,
    /// optimized strategies for Angular, Reactive etc can be added in the future.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    public interface IHud<TAvatar,TVector3> 
        where TVector3 : struct, IVector3
        where TAvatar : class, IAvatar<TVector3>, new()
    {
        /// <summary>
        /// Occurs when avatar clicks on click hud element.
        /// </summary>
        event OnClickHudElement<TAvatar, TVector3> OnClickHudElement;
        /// <summary>
        /// Calls the specified javascript function with parameters on the avatars hud.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="p">The p.</param>
        void Call(TAvatar avatar, string function, params string[] p);
        /// <summary>
        /// Sends data to the specified avatar hud screen.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="data">The data.</param>
        void Send(TAvatar avatar, string data);
        /// <summary>
        /// Sends data to an html element within the specified avatar hud screen.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        void Send(TAvatar avatar, string elementName, string attribute, string data);
        /// <summary>
        /// Sends data to html elements within the specified avatar hud screen and fills a certain attribute with data.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        void Send(TAvatar avatar, string[] elementName, string attribute, string[] data);
        /// <summary>
        /// Sends data to html elements within the specified avatar hud screen and fills a multiple attribute with data.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        void Send(TAvatar avatar, string[] elementName, string[] attribute, string[] data);
        /// <summary>
        /// Gets the data for the specified element attribute from the specified avatar hud screen.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        string Get(TAvatar avatar, string elementName, string attribute, string data);
        /// <summary>
        /// Gets the data for the specified elements attribute of from specified avatar hud screen.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        /// <returns>JSON Object</returns>
        string Get(TAvatar avatar, string[] elementName, string attribute, string[] data);
        /// <summary>
        /// Gets the data for the specified elements and attributes from the specified avatar hud screen.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="data">The data.</param>
        /// <returns>JSON Object</returns>
        string Get(TAvatar avatar, string[] elementName, string[] attribute, string[] data);

    }
}
