using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services.Models;

namespace WarehouseMenagementAPI.Helpers
{
    public class Create100RandomProducts
    {
        private readonly WarehouseDbContext _dbContext;

        public Create100RandomProducts(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRandomProducts(int warehouseId)
        {
            var warehouseExists = _dbContext.Warehouses.Any(w => w.WarehouseId == warehouseId);

            for (int i = 0; i < 100; i++)
            {
                if (!warehouseExists)
                {
                    Console.WriteLine("Warehouse not found!");
                }

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
                    AlleyId = alleyId,
                    WarehouseId = warehouseId
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
        }
    }
}
