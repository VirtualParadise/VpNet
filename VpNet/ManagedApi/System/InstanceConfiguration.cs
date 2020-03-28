using System;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    public class InstanceConfiguration<TWorld> : Abstract.BaseInstanceConfiguration<TWorld>
        where TWorld : class, IWorld, new()
    {
        public InstanceConfiguration(bool isChildInstance)
        {
            IsChildInstance = isChildInstance;
        }

        public InstanceConfiguration()
        {
            IsChildInstance = false;
        }
    }
}
