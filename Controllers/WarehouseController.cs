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
            for(int i = 0; i < 20; i++)
            {
                Product randomProduct = new Product
                {
                    Type = RandomProductGenerator.GenerateRandomProductType(),
                    Name = RandomProductGenerator.GenerateRandomProductName(),
                    Price = RandomProductGenerator.GenerateRandomProducPrice(),
                    PostalCode = RandomProductGenerator.GenerateRandomPostalCode()
                };

                bool productExists = _dbContext.Products
                    .Any(p => p.Name == randomProduct.Name && p.Type == randomProduct.Type);

                if (!productExists)
                {
                    _dbContext.Products.Add(randomProduct);
                }
                else
                {
                    var existingProduct = _dbContext.Products
                        .FirstOrDefault(p => p.Name == randomProduct.Name && p.Type == randomProduct.Type);

                    if (existingProduct != null)
                    {
                        randomProduct.Price = existingProduct.Price;
                        _dbContext.Products.Add(randomProduct);
                    }
                }
            }
            _dbContext.SaveChanges();
            return Ok("Random products was succesfuly added");
        }
    }
}
