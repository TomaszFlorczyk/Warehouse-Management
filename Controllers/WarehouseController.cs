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
            for(int i = 0; i < 100; i++)
            {
                string postalCode = RandomProductGenerator.GenerateRandomPostalCode();

                int alleyId = RandomProductGenerator.GenerateAlleyIdFromPostalCode(postalCode);
                bool alleyExists = _dbContext.Alleys.Any(a => a.Id == alleyId);

                if (!alleyExists)
                {
                    continue;
                }
                Product randomProduct = new Product
                {
                    Type = RandomProductGenerator.GenerateRandomProductType(),
                    Name = RandomProductGenerator.GenerateRandomProductName(),
                    Price = RandomProductGenerator.GenerateRandomProducPrice(),
                    PostalCode = postalCode,
                    AlleyId = alleyId
                };

                bool productExists = _dbContext.ProductDelivery
                    .Any(p => p.Name == randomProduct.Name && p.Type == randomProduct.Type);

                if (!productExists)
                {
                    _dbContext.ProductDelivery.Add(randomProduct);
                }
                else
                {
                    var existingProduct = _dbContext.ProductDelivery
                        .FirstOrDefault(p => p.Name == randomProduct.Name && p.Type == randomProduct.Type);

                    if (existingProduct != null)
                    {
                        randomProduct.Price = existingProduct.Price;
                        _dbContext.ProductDelivery.Add(randomProduct);
                    }
                }
            }
            _dbContext.SaveChanges();
            return Ok("Random products was succesfuly added");
        }
    }
}
