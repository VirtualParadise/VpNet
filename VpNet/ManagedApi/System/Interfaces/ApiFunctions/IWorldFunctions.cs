using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IWorldFunctions
    {
        void Wait(int milliseconds=0);
        Task EnterAsync(IWorld world);
        Task EnterAsync(string world);
        /// <summary>
        /// Enter world using instance configuration.
        /// </summary>
        /// <returns></returns>
        Task EnterAsync();
        void ListWorlds();
        void Leave();
    }
}
