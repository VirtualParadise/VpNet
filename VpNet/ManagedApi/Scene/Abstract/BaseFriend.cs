using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseFriend : IFriend
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Online { get; set; }
    }
}