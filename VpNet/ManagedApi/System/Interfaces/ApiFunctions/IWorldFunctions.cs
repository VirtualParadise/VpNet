using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IWorldFunctions<in TWorld> 
        where TWorld : class, IWorld, new()
    {
        void Wait(int milliseconds=0);
        Task EnterAsync(TWorld world);
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
