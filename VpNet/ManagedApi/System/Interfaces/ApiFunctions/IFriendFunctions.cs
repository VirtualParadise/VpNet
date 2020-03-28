namespace VpNet.Interfaces
{
    public interface IFriendFunctions<in TFriend>
        where TFriend : class, IFriend, new()
    {
        void GetFriends();
        void AddFriendByName(TFriend friend);
        void AddFriendByName(string name);
        void DeleteFriendById(int friendId);
        void DeleteFriendById(TFriend friend);
    }
}
