using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseUniverseDisconnectEventArgs
    {
        public IUniverse Universe { get; set; }

        public VpNet.DisconnectType DisconnectType { get; set; }

        protected BaseUniverseDisconnectEventArgs(IUniverse universe)
        {
            Universe = universe;
        }

        protected BaseUniverseDisconnectEventArgs()
        {
            
        } 
    }
}
