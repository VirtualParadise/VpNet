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

using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    /// <summary>
    /// Templated Event Arguments implementation.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("OnAvatarChange", Namespace = Global.XmlNsEvent)]
    public class AvatarChangeEventArgsT<TAvatar,TVector3> : Abstract.BaseAvatarChangeEventArgs<TAvatar,TVector3>
       where TVector3 : struct, IVector3
       where TAvatar : class, IAvatar<TVector3>, new()
    { }

    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("OnAvatarEnter", Namespace = Global.XmlNsEvent)]
    public class AvatarEnterEventArgsT<TAvatar,TVector3> : Abstract.BaseAvatarEnterEventArgs<TAvatar,TVector3>
       where TVector3 : struct, IVector3
       where TAvatar : class, IAvatar<TVector3>, new()
    { }
    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("OnAvatarLeave", Namespace = Global.XmlNsEvent)]
    public class AvatarLeaveEventArgsT<TAvatar, TVector3> : Abstract.BaseAvatarLeaveEventArgs<TAvatar, TVector3>
       where TVector3 : struct, IVector3
       where TAvatar : class, IAvatar<TVector3>, new()
    { }
    /// <summary>
    /// Templated Event Arguments implementation
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("OnAvatarClick", Namespace = Global.XmlNsEvent)]
    public class AvatarClickEventArgsT<TAvatar, TVector3> : Abstract.BaseAvatarClickEventArgs<TAvatar, TVector3>
        where TVector3 : struct, IVector3
        where TAvatar : class, IAvatar<TVector3>, new()
    { }

    [XmlRoot("OnJoin", Namespace = Global.XmlNsEvent)]
    public class JoinEventArgsT : Abstract.BaseJoinEventArgs { }
}
