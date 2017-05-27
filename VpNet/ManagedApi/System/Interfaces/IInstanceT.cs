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

using VpNet.Abstract;

namespace VpNet.Interfaces
{
    public interface IInstanceT<TImplementor,
/* Scene Type specifications ----------------------------------------------------------------------------------------------------------------------------------------------*/
        TAvatar, in TColor, in TFriend, TResult, TTerrainCell, TTerrainNode,
        in TTerrainTile, TVector3, in TVpObject, in TWorld, in TWorldAttributes,
        in TCell,TChatMessage,TTerrain,TUniverse,TTeleport,
/* Event Arg types --------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Avatar Event Args */
        TAvatarChangeEventArgs, TAvatarEnterEventArgs, TAvatarLeaveEventArgs,
        /* Cell Event Args */
        TQueryCellResultargs, TQueryCelEndArgs,
        /* Chat Event Args */
        TChatMessageEventArgs,
        /* Friend Event Args */
        TFriendAddCallbackEventArgs, TFriendDeleteCallbackEventArgs, TFriendsGetCallbackEventArgs,
        /* Terrain Event Args */
        TTerainNodeEventArgs,
        /* Universe Event Args */
        TUniverseDisconnectEventargs,
        /* VpObject Event Args */
        TObjectChangeArgs, TObjectChangeCallbackArgs, TObjectClickArgs, TObjectCreateArgs,
        TObjectCreateCallbackArgs, TObjectDeleteArgs, TObjectDeleteCallbackArgs,
        /* World Event Args */
        TWorldDisconnectEventArg, TWorldListEventargs, TWorldSettingsChangedEventArg,
        /* Teleport Event Args */
        TTeleportEventArgs
        > :
/* Interface specifications -----------------------------------------------------------------------------------------------------------------------------------------*/
        /* Functions */
        IAvatarFunctions<TResult, TAvatar, TVector3>,
        IChatFunctions<TResult, TAvatar, TColor, TVector3>,
        IFriendFunctions<TResult, TFriend>,
        ITeleportFunctions<TResult, TWorld, TAvatar, TVector3>,
        ITerrainFunctions<TResult, TTerrainTile, TTerrainNode, TTerrainCell>,
        IVpObjectFunctions<TResult, TVpObject, TVector3>,
        IWorldFunctions<TResult, TWorld, TWorldAttributes>,
        IUniverseFunctions<TResult> 
/* Constraints ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        where TTeleportEventArgs : class, ITeleportEventArgs<TTeleport,TWorld,TAvatar,TVector3>
        where TUniverse : class, IUniverse, new()
        where TTerrain : class, ITerrain, new()
        where TCell : class, ICell, new()
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile, TTerrainNode, TTerrainCell>, new()
        where TResult : class, IRc, new()
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar<TVector3>, new()
        where TFriend : class, IFriend, new()
        where TColor : class, IColor, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TVector3 : struct, IVector3
        where TWorldAttributes : class, IWorldAttributes, new()
        where TTeleport : class, ITeleport<TWorld,TAvatar,TVector3> , new()
        where TImplementor : class, new()
    {
        TImplementor Implementor { get; set; }
    }
}
