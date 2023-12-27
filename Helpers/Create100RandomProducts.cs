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

        public void CreateRandomProducts(int warehouseId)
        {
            for (int i = 0; i < 100; i++)
            {
                string postalCode = RandomProductGenerator.GenerateRandomPostalCode();
                int alleyId = RandomProductGenerator.GenerateAlleyIdFromPostalCode(postalCode);
                var alleyFromDB = _dbContext.Alleys.FirstOrDefault(a => a.AlleyId == alleyId);

                if (alleyFromDB is null)
                {
                    continue;
                }

                Product randomProduct = new Product
                {
                    Type = RandomProductGenerator.GenerateRandomProductType(),
                    Name = RandomProductGenerator.GenerateRandomProductName(),
                    Price = RandomProductGenerator.GenerateRandomProducPrice(),
                    PostalCode = postalCode,
                    Alley = alleyFromDB
                };

                var productFromDB = _dbContext.ProductDelivery
                    .FirstOrDefault(p => p.Name == randomProduct.Name && p.Type == randomProduct.Type);

                if (productFromDB is null)
                {
                    _dbContext.ProductDelivery.Add(randomProduct);
                }
                else
                {
                    randomProduct.Price = productFromDB.Price;
                    _dbContext.ProductDelivery.Add(randomProduct);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
