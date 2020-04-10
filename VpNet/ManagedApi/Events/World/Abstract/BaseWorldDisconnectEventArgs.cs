using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldDisconnectEventArgs : TimedEventArgs, IWorldDisconnectEventArgs
    {
        public IWorld World { get; set; }

        protected BaseWorldDisconnectEventArgs() { }

        protected BaseWorldDisconnectEventArgs(IWorld world)
        {
            World = world;
        }
    }
}
