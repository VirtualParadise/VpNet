using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseTeleport : ITeleport
    {
        public IWorld World { get; set; }
        public IAvatar Avatar { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        protected BaseTeleport(){}
 
    }
}
