using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Interfaces
{
    public interface IWarehouseService 
    {
        Task<Result> AddWarehouseAsync(string name, int warehouseId);
        Task<Result> RemoveWarehouseByIdAsync(int id);
        Task<Result> UpdateWarehouseAsync(int id, string name, int newId);
        Task<Result<Warehouse>> GetWarehouseByIdAsync(int id);
        Task<Result<List<Warehouse>>> GetAllWarehousesAsync();
    }
}
