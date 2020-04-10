using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTeleportEventArgs : TimedEventArgs, ITeleportEventArgs
    {
        public ITeleport Teleport { get; set; }

        protected BaseTeleportEventArgs() {}

        protected BaseTeleportEventArgs(ITeleport teleport)
        {
            Teleport = teleport;
        } 
    }
}
