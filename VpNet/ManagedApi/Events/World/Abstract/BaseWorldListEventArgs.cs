using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldListEventArgs : TimedEventArgs, IWorldListEventArgs
    {
        public IWorld World { get; set; }

        protected BaseWorldListEventArgs() { }

        protected BaseWorldListEventArgs(IWorld world)
        {
            World = world;
        }
    }
}
