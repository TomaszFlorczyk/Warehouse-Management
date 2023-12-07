using Microsoft.EntityFrameworkCore;
using WarehouseMenagementAPI.Models;

namespace WarehouseMenagementAPI.Helpers
{
    public class Create100RandomProducts
    {
        private readonly WarehouseDbContext _dbContext;

        public Create100RandomProducts(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRandomProducts()
        {
            for (int i = 0; i < 100; i++)
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
        }
    }
}
