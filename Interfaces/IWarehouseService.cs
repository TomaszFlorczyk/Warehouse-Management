using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Interfaces
{
    public interface IWarehouseService 
    {
        Task<Result> AddWarehouseAsync(string name, int warehouseId);
        Task<Result> RemoveWarehouseByIdAsync(int warehouseId);
        Task<Result> UpdateWarehouseAsync(int warehouseId, string name, int newId);
        Task<Result<Warehouse>> GetWarehouseByIdAsync(int warehouseId);
        Task<Result<List<Warehouse>>> GetAllWarehousesAsync();
    }
}
