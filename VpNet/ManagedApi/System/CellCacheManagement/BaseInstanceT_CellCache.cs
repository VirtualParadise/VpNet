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
using System.Collections.Generic;
using System.Linq;
using VpNet.Extensions;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    /// <summary>
    /// Abtract fully teamplated instance class, providing .NET encapsulation strict templated types to the native C wrapper.
    /// </summary>
    /// <typeparam name="T">Type of the abstract implementation</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTerrainCell">The type of the terrain cell.</typeparam>
    /// <typeparam name="TTerrainNode">The type of the terrain node.</typeparam>
    /// <typeparam name="TTerrainTile">The type of the terrain tile.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TWorldAttributes">The type of the world attributes.</typeparam>
    /// <typeparam name="TCell">The type of the cell.</typeparam>
    /// <typeparam name="TChatMessage">The type of the chat message.</typeparam>
    /// <typeparam name="TTerrain">The type of the terrain.</typeparam>
    /// <typeparam name="TUniverse">The type of the universe.</typeparam>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    public abstract partial class BaseInstanceT<T,
        /* Scene Type specifications ----------------------------------------------------------------------------------------------------------------------------------------------*/
        TAvatar, TColor, TFriend, TResult, TTerrainCell, TTerrainNode,
        TTerrainTile, TVector3, TVpObject, TWorld, TWorldAttributes, TCell, TChatMessage, TTerrain, TUniverse, TTeleport,
        TUserAttributes,THud
        > :
        /* Interface specifications -----------------------------------------------------------------------------------------------------------------------------------------*/
        /* Functions */
        BaseInstanceEvents<TWorld>,
        IAvatarFunctions<TResult, TAvatar, TVector3>,
        IChatFunctions<TResult, TAvatar, TColor, TVector3>,
        IFriendFunctions<TResult, TFriend>,
        ITeleportFunctions<TResult, TWorld, TAvatar, TVector3>,
        ITerrainFunctions<TResult, TTerrainTile, TTerrainNode, TTerrainCell>,
        IVpObjectFunctions<TResult, TVpObject, TVector3>,
        IWorldFunctions<TResult, TWorld, TWorldAttributes>,
        IUniverseFunctions<TResult>
        /* Constraints ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        where TUniverse : class, IUniverse, new()
        where TTerrain : class, ITerrain, new()
        where TCell : class, ICell, new()
        where TChatMessage : class, IChatMessage<TColor>, new()
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile, TTerrainNode, TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile, TTerrainNode, TTerrainCell>, new()
        where TResult : class, IRc, new()
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar<TVector3>, new()
        where TFriend : class, IFriend, new()
        where TColor : class, IColor, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TVector3 : struct, IVector3
        where TWorldAttributes : class, IWorldAttributes, new()
        where TTeleport : class, ITeleport<TWorld, TAvatar, TVector3>, new()
        where TUserAttributes : class, IUserAttributes, new()
        where THud : IHud<TAvatar,TVector3>
        where T : class, new()
    {
        public delegate void CellRangeQueryCompletedDelegate(T sender, CellRangeQueryCompletedArgs<TVpObject,TVector3> args);
        public delegate void CellRangeObjectChangedDelegate(T sender, ObjectChangeArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void CellRangeObjectDeletedDelegate(T sender, ObjectDeleteArgsT<TAvatar, TVpObject, TVector3> args);
        public delegate void CellRangeObjectCreatedDelegate(T sender, ObjectCreateArgsT<TAvatar, TVpObject, TVector3> args);

        public event CellRangeQueryCompletedDelegate OnQueryCellRangeEnd;
        public event CellRangeObjectChangedDelegate OnObjectCellRangeChange;
        public event CellRangeObjectChangedDelegate OnObjectCellRangeDelete;
        public event CellRangeObjectChangedDelegate OnObjectCellRangeCreate;

        private List<TVpObject> _objects; 
        private List<TCell> _cache;
        private bool _isScanning;

        private List<TCell> _cacheScanned;
        private bool _useCellCache;
        private List<TCell> _cacheScanning;

        public List<TVpObject> CacheObjects { get { return _objects; } } 

        public int Cells { 
            get { return _cache.Count(); }
        }

        public bool UseCellCache
        {
            get { return _useCellCache; }
            set
            {
                if (value == _useCellCache)
                    return;
                if (_isScanning)
                    throw new Exception("Please do not change the use cache boolean flag while previous scan has not been completed.");
                if (value)
                {
                    _objects = new List<TVpObject>();
                    _cache = new List<TCell>();
                    _cacheScanned = new List<TCell>();
                    _cacheScanning = new List<TCell>();

                    OnQueryCellResult += BaseInstanceT_CellCache_OnQueryCellResult;
                    OnQueryCellEnd += BaseInstanceT_CellCache_OnQueryCellEnd;
                    OnObjectChange += BaseInstanceT_CellCache_OnObjectChange;
                    OnObjectDelete += BaseInstanceT_OnObjectDelete;
                    OnObjectCreate += BaseInstanceT_OnObjectCreate;
                }
                else
                {
                    OnQueryCellResult -= BaseInstanceT_CellCache_OnQueryCellResult;
                    OnQueryCellEnd -= BaseInstanceT_CellCache_OnQueryCellEnd;
                    OnObjectChange -= BaseInstanceT_CellCache_OnObjectChange;
                }
                _useCellCache = value;
            }
        }

        bool IsInCellCacheRange(TVpObject vpObject)
        {
            return  _cacheScanned.Exists(p => p.X == vpObject.Cell.X && p.Z == vpObject.Cell.Z);
        }

        void BaseInstanceT_OnObjectCreate(T sender, ObjectCreateArgsT<TAvatar, TVpObject, TVector3> args)
        {
            if (!IsInCellCacheRange(args.VpObject))
                return;
            _objects.Add(args.VpObject);

        }

        void BaseInstanceT_OnObjectDelete(T sender, ObjectDeleteArgsT<TAvatar, TVpObject, TVector3> args)
        {
            if (!IsInCellCacheRange(args.VpObject))
                return;
            var o = _objects.Find(p => p.Id == args.VpObject.Id);
            _objects.Remove(o);
        }

        void BaseInstanceT_CellCache_OnObjectChange(T sender, ObjectChangeArgsT<TAvatar, TVpObject, TVector3> args)
        {
            lock (this)
            {
                if (!IsInCellCacheRange(args.VpObject))
                {
                    // check if object was in cell range prior.
                    var prev = _objects.Find(p => p.Id == args.VpObject.Id);
                    _objects.Remove(prev);
                    return;
                }
                var o = _objects.Find(p => p.Id == args.VpObject.Id);
                _objects.Remove(o);
                _objects.Add(args.VpObject);
                if (OnObjectCellRangeChange != null)
                    OnObjectCellRangeChange(Implementor, args);
            }
        }

        void BaseInstanceT_CellCache_OnQueryCellEnd(T sender, QueryCellEndArgsT<TCell> args)
        {
            lock (this)
            {
                _cacheScanning.RemoveAll(p => p.X == args.Cell.X && p.Z == args.Cell.Z);
                _cacheScanned.Add(args.Cell);
                if (_cache.Count == 0)
                {
                    _isScanning = false;
                    if (OnQueryCellRangeEnd != null)
                        OnQueryCellRangeEnd(Implementor, new CellRangeQueryCompletedArgs<TVpObject,TVector3> { VpObjects = _objects.Copy() });
                }
                else
                {
                    _cacheScanning.Add(_cache[0]);
                    QueryCell(_cache[0].X, _cache[0].Z);
                    _cache.RemoveAt(0);
                }
            }
        }

        void BaseInstanceT_CellCache_OnQueryCellResult(T sender, QueryCellResultArgsT<TVpObject, TVector3> args)
        {
            lock (this)
            {
                _objects.Add(args.VpObject);
            }
        }


        private bool IsCellInList(TCell cell)
        {
            return ((_cache.Find(p => p.X == cell.X && p.Z == cell.Z) != null) ||
                    (_cacheScanned.Find(p => p.X == cell.X && p.Z == cell.Z) != null));
        }

        public int AddCellRange(TCell start, TCell end)
        {
            if (_isScanning)
                throw new Exception("Can not issue a cell range query before the other range query has ended.");
            if (!UseCellCache)
                UseCellCache = true;
            lock (this)
            {
                var l = CreateQueryList(start, end);
                foreach (TCell cell in l)
                {

                    if (!IsCellInList(cell))
                    {
                        _cache.Add(cell);
                    }
                }
                if (l.Count>0)
                {
                    if (!_isScanning)
                    {
                        _isScanning = true;
                        for (int i = 0; i < 64; i++)
                        {
                            if (_cache.Count == 0)
                                break;
                            _cacheScanning.Add(_cache[0]);
                            QueryCell(_cache[0].X, _cache[0].Z);
                            _cache.Remove(_cache[0]);
                        }
                    }
                }
                return l.Count();
            }
        }

        private List<TCell> CreateQueryList(TCell start, TCell end)
        {
            var ret = new List<TCell>();
            var listX = new List<TCell> { start, end }.OrderBy(p => p.X);
            var listY = new List<TCell> { start, end }.OrderBy(p => p.Z);
            var p1 = new Cell(listX.ElementAt(0).X, listY.ElementAt(0).Z);
            var p2 = new Cell(listX.ElementAt(1).X, listY.ElementAt(1).Z);
            for (var x = p1.X; x < p2.X; x++)
            {
                for (var z = p1.Z; z < p2.Z; z++)
                {
                    ret.Add(new TCell{X=x, Z= z});
                }
            }

            return ret;
        }


        public void AddCell(TCell cell)
        {
            if (_isScanning)
                throw new Exception("Can not issue a cell query before the other range query has ended.");
            if (!UseCellCache)
                UseCellCache = true;

            lock (this)
            {
                if (!IsCellInList(cell))
                {
                    _cache.Add(cell);
                    if (!_isScanning)
                    {
                        _isScanning = true;
                        QueryCell(cell.X, cell.Z);
                    }
                }
            }
        }
    }
}
