using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseFriend : IFriend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Online { get; set; }
    }
}