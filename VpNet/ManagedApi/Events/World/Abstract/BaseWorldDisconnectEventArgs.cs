using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldDisconnectEventArgs<TWorld> : TimedEventArgs, IWorldDisconnectEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }

        protected BaseWorldDisconnectEventArgs() { }

        protected BaseWorldDisconnectEventArgs(TWorld world)
        {
            World = world;
        }
    }
}
