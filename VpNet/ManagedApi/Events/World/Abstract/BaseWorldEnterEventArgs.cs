using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldEnterEventArgs : TimedEventArgs
    {
        public IWorld World { get; set; }

        protected BaseWorldEnterEventArgs() { }

        protected BaseWorldEnterEventArgs(IWorld world)
        {
            World = world;
        }
    }
}
