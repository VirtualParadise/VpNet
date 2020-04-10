namespace VpNet.Interfaces
{
    public interface IFriendFunctions
    {
        void GetFriends();
        void AddFriendByName(IFriend friend);
        void AddFriendByName(string name);
        void DeleteFriendById(int friendId);
        void DeleteFriendById(IFriend friend);
    }
}
