using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseMenagementAPI.Interfaces;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Services
{
    public class AlleyService : IAlleyService
    {
        private readonly WarehouseDbContext _dbContext;

        public AlleyService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> AddAlleyAsync(string name, int warehouseId)
        {
            if (string.IsNullOrEmpty(name) && int.IsEvenInteger(warehouseId))
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Invalid Alley Data!"
                };
            }
            var alleyNameExists = _dbContext.Alleys.Any(a => a.Name == name);

            if (alleyNameExists)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "AlleyName already exists!"
                };
            }

            var alleyToCreate = new Alley { Name = name, WarehouseId = warehouseId};

            try
            {
                await _dbContext.AddAsync(alleyToCreate);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Result
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = $"Could not add alley. Excepction occured. Exception message: {e.Message}"
                };
            }

            return new Result
            {
                HttpStatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Alley created successfully!"
            };
        }

        public Task<Result> GetAllAlleys()
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetAlleyByIdAsync(int alleyId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetAlleyByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Result> RemoveAlleyAsync(int alleyId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAlley(int alleyId, string newName, int newId)
        {
            throw new NotImplementedException();
        }
    }
}
