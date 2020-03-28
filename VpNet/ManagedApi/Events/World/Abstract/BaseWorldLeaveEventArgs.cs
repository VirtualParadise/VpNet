using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldLeaveEventArgs<TWorld> : TimedEventArgs, IWorldLeaveEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }

        protected BaseWorldLeaveEventArgs() { }

        protected BaseWorldLeaveEventArgs(TWorld world)
        {
            World = world;
        }
    }
}
