using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTeleportEventArgs<TTeleport, TWorld, TAvatar> : TimedEventArgs, ITeleportEventArgs<TTeleport, TWorld, TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar, new()
        where TTeleport : class, ITeleport<TWorld,TAvatar>, new()
    {
        public TTeleport Teleport { get; set; }

        protected BaseTeleportEventArgs() {}

        protected BaseTeleportEventArgs(TTeleport teleport)
        {
            Teleport = teleport;
        } 
    }
}
