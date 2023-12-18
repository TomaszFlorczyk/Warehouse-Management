using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Interfaces
{
    public interface IAlleyService
    {
        Task<Result> AddAlleyAsync(string name, int warehouseId);
        Task<Result> RemoveAlleyAsync(int alleyId);
        Task<Result> GetAllAlleys();
        Task<Result> GetAlleyByIdAsync(int alleyId);
        Task<Result> GetAlleyByNameAsync(string name);
        Task<Result> UpdateAlley(int alleyId, string newName, int newId);
    }
}
