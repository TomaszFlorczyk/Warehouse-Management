using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseMenagementAPI.Exceptions;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Services
{
    public class WarehouseService
    {
        private readonly WarehouseDbContext _dbContext;

        public WarehouseService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> AddWarehouseAsync(string name)
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

            var warehouseExist = _dbContext.Warehouses.Any(w => w.Name == name);

            if (warehouseExist)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Warehouse with this name already exists!"
                };
            }
            
            var warehouse = new Warehouse { Name = name };

            try
            {
                await _dbContext.Warehouses.AddAsync(warehouse);
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
    }
}
