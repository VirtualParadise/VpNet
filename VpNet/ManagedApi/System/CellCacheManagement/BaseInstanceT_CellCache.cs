﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VpNet
{
    public partial class VirtualParadiseClient
    {
        public delegate void CellRangeQueryCompletedDelegate(VirtualParadiseClient sender, CellRangeQueryCompletedArgs args);
        public delegate void CellRangeObjectChangedDelegate(VirtualParadiseClient sender, ObjectChangeArgs args);
        public delegate void CellRangeObjectDeletedDelegate(VirtualParadiseClient sender, ObjectDeleteArgs args);
        public delegate void CellRangeObjectCreatedDelegate(VirtualParadiseClient sender, ObjectCreateArgs args);

        public event CellRangeQueryCompletedDelegate OnQueryCellRangeEnd;
        public event CellRangeObjectChangedDelegate OnObjectCellRangeChange;

        private List<VpObject> _objects; 
        private List<Cell> _cache;
        private bool _isScanning;

        private List<Cell> _cacheScanned;
        private bool _useCellCache;
        private List<Cell> _cacheScanning;

        public List<VpObject> CacheObjects { get { return _objects; } } 

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
                    _objects = new List<VpObject>();
                    _cache = new List<Cell>();
                    _cacheScanned = new List<Cell>();
                    _cacheScanning = new List<Cell>();

                    QueryCellResult += BaseInstanceTCellCacheQueryCellResult;
                    QueryCellEnd += BaseInstanceTCellCacheQueryCellEnd;
                    ObjectChanged += BaseInstanceTCellCacheObjectChanged;
                    ObjectDeleted += BaseInstanceTObjectDeleted;
                    ObjectCreated += BaseInstanceTObjectCreated;
                }
                else
                {
                    QueryCellResult -= BaseInstanceTCellCacheQueryCellResult;
                    QueryCellEnd -= BaseInstanceTCellCacheQueryCellEnd;
                    ObjectChanged -= BaseInstanceTCellCacheObjectChanged;
                }
                _useCellCache = value;
            }
        }

        bool IsInCellCacheRange(VpObject vpObject)
        {
            return  _cacheScanned.Exists(p => p.X == vpObject.Cell.X && p.Z == vpObject.Cell.Z);
        }

        void BaseInstanceTObjectCreated(VirtualParadiseClient sender, ObjectCreateArgs args)
        {
            if (!IsInCellCacheRange(args.Object))
                return;
            _objects.Add(args.Object);

        }

        void BaseInstanceTObjectDeleted(VirtualParadiseClient sender, ObjectDeleteArgs args)
        {
            if (!IsInCellCacheRange(args.Object))
                return;
            var o = _objects.Find(p => p.Id == args.Object.Id);
            _objects.Remove(o);
        }

        void BaseInstanceTCellCacheObjectChanged(VirtualParadiseClient sender, ObjectChangeArgs args)
        {
            lock (this)
            {
                if (!IsInCellCacheRange(args.Object))
                {
                    // check if object was in cell range prior.
                    var prev = _objects.Find(p => p.Id == args.Object.Id);
                    _objects.Remove(prev);
                    return;
                }
                var o = _objects.Find(p => p.Id == args.Object.Id);
                _objects.Remove(o);
                _objects.Add(args.Object);
                OnObjectCellRangeChange?.Invoke(this, args);
            }
        }

        void BaseInstanceTCellCacheQueryCellEnd(VirtualParadiseClient sender, QueryCellEndArgs args)
        {
            lock (this)
            {
                _cacheScanning.RemoveAll(p => p.X == args.Cell.X && p.Z == args.Cell.Z);
                _cacheScanned.Add(args.Cell);
                if (_cache.Count == 0)
                {
                    _isScanning = false;
                    OnQueryCellRangeEnd?.Invoke(this, new CellRangeQueryCompletedArgs(_objects));
                }
                else
                {
                    _cacheScanning.Add(_cache[0]);
                    QueryCell(_cache[0].X, _cache[0].Z);
                    _cache.RemoveAt(0);
                }
            }
        }

        void BaseInstanceTCellCacheQueryCellResult(VirtualParadiseClient sender, QueryCellResultArgs args)
        {
            lock (this)
            {
                _objects.Add(args.Object);
            }
        }


        private bool IsCellInList(Cell cell)
        {
            return ((_cache.Exists(p => p.X == cell.X && p.Z == cell.Z)) ||
                    (_cacheScanned.Exists(p => p.X == cell.X && p.Z == cell.Z)));
        }

        public int AddCellRange(Cell start, Cell end)
        {
            if (_isScanning)
                throw new Exception("Can not issue a cell range query before the other range query has ended.");
            if (!UseCellCache)
                UseCellCache = true;
            lock (this)
            {
                var l = CreateQueryList(start, end);
                foreach (Cell cell in l)
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

        private List<Cell> CreateQueryList(Cell start, Cell end)
        {
            var ret = new List<Cell>();
            var listX = new List<Cell> { start, end }.OrderBy(p => p.X);
            var listY = new List<Cell> { start, end }.OrderBy(p => p.Z);
            var p1 = new Cell(listX.ElementAt(0).X, listY.ElementAt(0).Z);
            var p2 = new Cell(listX.ElementAt(1).X, listY.ElementAt(1).Z);
            for (var x = p1.X; x < p2.X; x++)
            {
                for (var z = p1.Z; z < p2.Z; z++)
                {
                    ret.Add(new Cell(x, z));
                }
            }

            return ret;
        }


        public void AddCell(Cell cell)
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
