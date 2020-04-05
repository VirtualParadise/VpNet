using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldSettingsChangedEventArgs : TimedEventArgs, IWorldSettingsChangedEventArgs
    {
        public IWorld World { get; set; }

        protected BaseWorldSettingsChangedEventArgs() { }

        protected BaseWorldSettingsChangedEventArgs(IWorld world)
        {
            World = world;
        }
    }
}
