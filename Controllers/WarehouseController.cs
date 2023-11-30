using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseMenagementAPI.Helpers;
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

        [HttpPost("AddRandomProducts")]
        public IActionResult AddRandomProdducts()
        {
            int i = 0;

            while(i  <= 20)
            {
                Product randomProduct = new Product
                {
                    Type = RandomProductGenerator.GenerateRandomProductType(),
                    Name = RandomProductGenerator.GenerateRandomProductName(),
                    Price = RandomProductGenerator.GenerateRandomProducPrice(),
                    PostalCode = RandomProductGenerator.GenerateRandomPostalCode()
                };

                _dbContext.Products.Add(randomProduct);
                _dbContext.SaveChanges();
                i++;
            }
            return Ok("Random products was succesfuly added");
        }

    }
}
