using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldEnterEventArgs<TWorld> : TimedEventArgs, IWorldEnterEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }

        protected BaseWorldEnterEventArgs() { }

        protected BaseWorldEnterEventArgs(TWorld world)
        {
            World = world;
        }
    }
}
