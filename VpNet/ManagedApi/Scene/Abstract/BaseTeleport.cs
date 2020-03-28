using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTeleport<TWorld,TAvatar> : ITeleport<TWorld,TAvatar>
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar,new()
    {
        public TWorld World { get; set; }
        public TAvatar Avatar { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        protected BaseTeleport(){}
 
    }
}
