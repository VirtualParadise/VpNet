using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IVpObjectFunctions
    {
        Task ChangeObjectAsync(IVpObject vpObject);
        Task<int> LoadObjectAsync(IVpObject vpObject);
        Task<int> AddObjectAsync(IVpObject vpObject);
        Task DeleteObjectAsync(IVpObject vpObject);
        void QueryCell(int cellX, int cellZ);
        void ClickObject(IVpObject vpObject);
        void ClickObject(int objectId);
    }
}
