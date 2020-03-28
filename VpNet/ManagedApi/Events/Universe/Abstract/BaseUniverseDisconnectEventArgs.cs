using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseUniverseDisconnectEventArgs<TUniverse> : TimedEventArgs, IUniverseDisconnectEventArgs<TUniverse> where TUniverse : class, IUniverse, new()
    {
        public TUniverse Universe { get; set; }

        public VpNet.DisconnectType DisconnectType { get; set; }

        protected BaseUniverseDisconnectEventArgs(TUniverse universe)
        {
            Universe = universe;
        }

        protected BaseUniverseDisconnectEventArgs()
        {
            
        } 
    }
}
