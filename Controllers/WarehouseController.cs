using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseMenagementAPI.Models;

namespace WarehouseMenagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseDbContext _dbContext;

        public WarehouseController(WarehouseDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

    }
}
