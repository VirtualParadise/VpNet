using System.Threading.Tasks;

namespace VpNet.Interfaces
{
    public interface IVpObjectFunctions<in TVpObject> 
        where TVpObject: class, IVpObject, new()
    {
        Task ChangeObjectAsync(TVpObject vpObject);
        Task<int> LoadObjectAsync(TVpObject vpObject);
        Task<int> AddObjectAsync(TVpObject vpObject);
        Task DeleteObjectAsync(TVpObject vpObject);
        void QueryCell(int cellX, int cellZ);
        void ClickObject(TVpObject vpObject);
        void ClickObject(int objectId);
    }
}
