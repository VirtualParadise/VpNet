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
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectChange",Namespace = Global.XmlNsEvent)]
    public class ObjectChangeArgsT<TAvatar, TVpObject, TVector3> : Abstract.BaseObjectChangeArgs<TAvatar, TVpObject, TVector3> 
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectChangeCallback", Namespace = Global.XmlNsEvent)]
    public class ObjectChangeCallbackArgsT<TResult, TVpObject, TVector3> : Abstract.BaseObjectChangeCallbackArgs<TResult, TVpObject, TVector3>
        where TResult : class, IRc, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()    
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectClick", Namespace = Global.XmlNsEvent)]
    public class ObjectClickArgsT<TAvatar, TVpObject, TVector3> : Abstract.BaseObjectClickArgs<TAvatar, TVpObject, TVector3>
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectCreate", Namespace = Global.XmlNsEvent)]
    public class ObjectCreateArgsT<TAvatar, TVpObject, TVector3> : Abstract.BaseObjectCreateArgs<TAvatar, TVpObject, TVector3>
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectCreateCallback", Namespace = Global.XmlNsEvent)]
    public class ObjectCreateCallbackArgsT<TResult, TVpObject, TVector3> : Abstract.BaseObjectCreateCallbackArgs<TResult, TVpObject, TVector3> 
        where TResult : class, IRc, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectDelete", Namespace = Global.XmlNsEvent)]
    public class ObjectDeleteArgsT<TAvatar, TVpObject, TVector3> : Abstract.BaseObjectDeleteArgs<TAvatar, TVpObject, TVector3>
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectDeleteCallback", Namespace = Global.XmlNsEvent)]
    public class ObjectDeleteCallbackArgsT<TResult, TVpObject, TVector3> : Abstract.BaseObjectDeleteCallbackArgs<TResult, TVpObject, TVector3>
        where TResult : class, IRc, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectGetCallback", Namespace = Global.XmlNsEvent)]
    public class ObjectGetCallbackArgsT<TResult, TVpObject, TVector3> : Abstract.BaseObjectGetCallbackArgs<TResult, TVpObject, TVector3>
        where TResult : class, IRc, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new()
    { }
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnObjectBump", Namespace = Global.XmlNsEvent)]
    public class ObjectBumpArgsT<TAvatar, TVpObject, TVector3> : Abstract.BaseObjectBumpArgs<TAvatar, TVpObject, TVector3>
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVector3 : struct, IVector3
        where TVpObject : class, IVpObject<TVector3>, new() { }
}
