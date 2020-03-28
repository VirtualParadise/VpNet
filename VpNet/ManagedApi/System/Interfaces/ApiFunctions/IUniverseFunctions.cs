using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IUniverseFunctions
    {
        Task ConnectAsync(string host = "universe.virtualparadise.org", ushort port = 57000);
        Task LoginAsync(string username, string password, string botname);
        /// <summary>
        /// Logs in the user, using the preloaded instance configuration.
        /// </summary>
        Task LoginAsync();
        /// <summary>
        /// Logs in to the universe and automatically enters the world using the preloaded instance configiguration.
        /// </summary>
        /// <param name="isAnnounceAvatar">if set to <c>true</c> [is announce avatar] then the avatar is updated on the given position as specified within the instance configuration. If the position is not specified, the avatar will appear at Ground Zero.</param>
        Task LoginAndEnterAsync(bool isAnnounceAvatar = true);
    }
}
