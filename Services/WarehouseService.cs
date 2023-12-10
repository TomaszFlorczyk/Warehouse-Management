using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using WarehouseMenagementAPI.Interfaces;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly WarehouseDbContext _dbContext;

        public WarehouseService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> AddWarehouseAsync(string name, int warehouseId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Invalid warehouse data!"
                };
            }

            var warehouseNameExist = _dbContext.Warehouses.Any(w => w.Name == name);
            var warehouseIdExist = _dbContext.Warehouses.Any(w => w.WarehouseId == warehouseId);

            if (warehouseNameExist)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Warehouse with this name already exists!"
                };
            }
            else if(warehouseIdExist)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Warehouse with this Id already exists!"
                };
            }

            var warehouseToCreate = new Warehouse { Name = name, WarehouseId = warehouseId};

            try
            {
                await _dbContext.Warehouses.AddAsync(warehouseToCreate);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Could not add warehouse. Excepction occured. Exception message: {e.Message}"
                };
            }
            
            return new Result
            {
                HttpStatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Warehouse added succesfully!"
            };
        }

        public async Task<Result<List<Warehouse>>> GetAllWarehousesAsync()
        {
            try
            {
               var warehouses = await _dbContext.Warehouses.ToListAsync();

                return new Result<List<Warehouse>>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Message = "The list of current warehouses in data!",
                    Data = warehouses
                };
            }
            catch (Exception e)
            {
                return new Result<List<Warehouse>>
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Internal server error: {e.Message}"
                };
            }
        }

        public async Task<Result<Warehouse>> GetWarehouseByIdAsync(int id)
        {
            try
            {
                var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId == id);

                if (warehouse != null)
                {
                    return new Result<Warehouse>
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Message = "Choosed warehouse in data!",
                        Data = warehouse
                    };
                }
                else
                {
                    return new Result<Warehouse>
                    {
                        HttpStatusCode = HttpStatusCode.InternalServerError,
                        IsSuccess = false,
                        Message = "Warehouse not found!"
                    };
                }
            }
            catch (Exception e)
            {
                return new Result<Warehouse>
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Internal server error: {e.Message}"
                };
            }
        }

        public async Task<Result> RemoveWarehouseByIdAsync(int id)
        {
            var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId == id);

            if (warehouse == null)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Warehouse not found!"
                };
            }

            try
            {
                 _dbContext.Warehouses.Remove(warehouse);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Could not delete warehouse. Exception occured. Exception message: {e.Message}"
                };
            }

            return new Result
            {
                HttpStatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Warehouse delete successfully"
            };
        }

        public async Task<Result> UpdateWarehouseAsync(int warehouseId, string newName, int newId)
        {
            var existingWarehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

            if (existingWarehouse == null)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Warehouse not found!"
                };
            }

            var isNameOrIdAlreadyExist = _dbContext.Warehouses.Any(w => w.Name == newName && w.WarehouseId == newId);

            if (isNameOrIdAlreadyExist)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Warehouse already exists!"
                };
            }

            existingWarehouse.WarehouseId = newId;
            existingWarehouse.Name = newName;

            /*if (existingWarehouse.WarehouseId == newId && existingWarehouse.Name != newName)
            {
                existingWarehouse.Name = newName;
            }
            else if (existingWarehouse.WarehouseId != newId && existingWarehouse.Name == newName)
            {
                existingWarehouse.WarehouseId = newId;
            }*/

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Could not update warehouse. Exception occured. Exception message {e.Message}"
                };
            }

            return new Result
            {
                HttpStatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Warehouse updated successfully"
            };
        }
    }
}
