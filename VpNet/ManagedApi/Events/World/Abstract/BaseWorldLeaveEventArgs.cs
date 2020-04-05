using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldLeaveEventArgs : TimedEventArgs
    {
        public IWorld World { get; set; }

        protected BaseWorldLeaveEventArgs() { }

        protected BaseWorldLeaveEventArgs(IWorld world)
        {
            World = world;
        }
    }
}
