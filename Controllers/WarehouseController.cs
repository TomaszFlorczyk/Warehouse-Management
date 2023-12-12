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
        public async Task<IActionResult> AddRandomProdducts([FromQuery]int warehouseId)
        {
            var result = await _warehouseService.AddRandomProducts(warehouseId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddWarehouse")]
        public async Task<IActionResult> AddWarehouse([FromQuery] string name, int warehouseId)
        {
            var result = await _warehouseService.AddWarehouseAsync(name, warehouseId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("RemoveWarehouseById")]
        public async Task<IActionResult> RemoveWarehouseById([FromQuery] int warehoused)
        {
            var warehouseToDelete = await _warehouseService.RemoveWarehouseByIdAsync(warehoused);

            if (warehouseToDelete.IsSuccess)
            {
                return Ok(warehouseToDelete);
            }

            return BadRequest(warehouseToDelete);
        }

        [HttpGet("GetAllWarehouses")]
        public async Task<IActionResult> GetAllWarehouse()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();

            if (warehouses.IsSuccess)
            {
                return Ok(warehouses);
            }

            return BadRequest(warehouses);
        }

        [HttpGet("GetWarehouseById")]
        public async Task<IActionResult> GetWarehouseById([FromQuery] int warehouseId)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
            if (warehouse.IsSuccess)
            {
                return Ok(warehouse);
            }
            return BadRequest(warehouse);
        }

        [HttpPut("UpdateWarehouse")]
        public async Task<IActionResult> UpdateWarehouse([FromQuery] int id, string newName, int newId)
        {
            var warehouseToUpdate = await _warehouseService.UpdateWarehouseAsync(id, newName, newId);
            if (warehouseToUpdate.IsSuccess)
            {
                return Ok(warehouseToUpdate);
            }
            return BadRequest(warehouseToUpdate);
        }
    }
}
