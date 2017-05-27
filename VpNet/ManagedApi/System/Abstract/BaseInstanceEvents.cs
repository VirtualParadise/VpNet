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
using VpNet.Interfaces;
using VpNet.NativeApi;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseInstanceEvents<TWorld>
        where TWorld : class, IWorld, new()
    {
        internal IntPtr _instance;
        //internal InstanceConfiguration<TWorld> _configuration;
        public InstanceConfiguration<TWorld> Configuration { get; set; }

        #region Implementation of IInstanceEvents

        internal abstract event EventDelegate OnChatNativeEvent;
        internal abstract event EventDelegate OnAvatarAddNativeEvent;
        internal abstract event EventDelegate OnAvatarDeleteNativeEvent;
        internal abstract event EventDelegate OnAvatarChangeNativeEvent;
        internal abstract event EventDelegate OnAvatarClickNativeEvent; 
        internal abstract event EventDelegate OnWorldListNativeEvent;
        internal abstract event EventDelegate OnObjectChangeNativeEvent;
        internal abstract event EventDelegate OnObjectCreateNativeEvent;
        internal abstract event EventDelegate OnObjectDeleteNativeEvent;
        internal abstract event EventDelegate OnObjectClickNativeEvent;
        internal abstract event EventDelegate OnObjectBumpNativeEvent;
        internal abstract event EventDelegate OnObjectBumpEndNativeEvent; 
        internal abstract event EventDelegate OnQueryCellEndNativeEvent;
        internal abstract event EventDelegate OnUniverseDisconnectNativeEvent;
        internal abstract event EventDelegate OnWorldDisconnectNativeEvent;
        internal abstract event EventDelegate OnTeleportNativeEvent;
        internal abstract event EventDelegate OnUserAttributesNativeEvent;
        internal abstract event EventDelegate OnJoinNativeEvent;

        internal abstract event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        internal abstract event CallbackDelegate OnObjectGetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnFriendAddCallbackNativeEvent;
        internal abstract event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        internal abstract event CallbackDelegate OnGetFriendsCallbackNativeEvent;

        internal abstract event CallbackDelegate OnObjectLoadCallbackNativeEvent;
        internal abstract event CallbackDelegate OnJoinCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldPermissionUserSetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldPermissionSessionSetCallbackNativeEvent;
        internal abstract event CallbackDelegate OnWorldSettingsSetCallbackNativeEvent;
        #endregion
    }
}