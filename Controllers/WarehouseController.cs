using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseMenagementAPI.Exceptions;
using WarehouseMenagementAPI.Helpers;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly WarehouseService _warehouseService;

        public WarehouseController(WarehouseDbContext dbContext, WarehouseService warehouseService)
        {
            _dbContext = dbContext;
            _warehouseService = warehouseService;
        }

        [HttpPost("AddRandomProducts")]
        public IActionResult AddRandomProdducts()
        {
            var createRandomProducts = new Create100RandomProducts(_dbContext);
            createRandomProducts.CreateRandomProducts();
            _dbContext.SaveChanges();
            return Ok("Random products was succesfuly added");
        }

        [HttpPost("AddWarehouse")]
        public async Task<IActionResult> AddWarehouse([FromQuery] string name)
        {
            var result =  await _warehouseService.AddWarehouseAsync(name);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("RemoveWarehouseById")]
        public async Task<IActionResult> RemoveWarehouseById([FromQuery] int id)
        {
            var warehouseToDelete = await _warehouseService.RemoveWarehouseByIdAsync(id);

            if (warehouseToDelete.IsSuccess)
            {
                return Ok(warehouseToDelete);
            }

            return BadRequest(warehouseToDelete);
        }
    }
}
