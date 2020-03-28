using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseWorldSettingsChangedEventArgs<TWorld> : TimedEventArgs, IWorldSettingsChangedEventArgs<TWorld> where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }

        protected BaseWorldSettingsChangedEventArgs() { }

        protected BaseWorldSettingsChangedEventArgs(TWorld world)
        {
            World = world;
        }
    }
}
