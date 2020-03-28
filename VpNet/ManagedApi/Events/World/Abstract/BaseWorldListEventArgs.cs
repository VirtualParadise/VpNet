using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldListEventArgs<TWorld> : TimedEventArgs, IWorldListEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }

        protected BaseWorldListEventArgs() { }

        protected BaseWorldListEventArgs(TWorld world)
        {
            World = world;
        }
    }
}
