using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services;

namespace WarehouseMenagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlleyController : ControllerBase
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly AlleyService _alleyService;

        public AlleyController(WarehouseDbContext dbContext, AlleyService alleyService)
        {
            _dbContext = dbContext;
            _alleyService = alleyService;
        }

        [HttpPost("AddAlley")]
        public async Task<IActionResult> AddAlley([FromQuery] string name, int alleyId, int warehouseId)
        {
            var result = await _alleyService.AddAlleyAsync(name, alleyId, warehouseId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllAlleys")]
        public async Task<IActionResult> GetAllAlleys()
        {
            var result = await _alleyService.GetAllAlleysAsync();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
