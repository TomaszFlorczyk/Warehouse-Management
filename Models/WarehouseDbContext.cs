using Microsoft.EntityFrameworkCore;

namespace WarehouseMenagementAPI.Models
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) 
        { 

        }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> ProductDelivery { get; set; }
        public DbSet<SentProducts> SentProducts { get; set; }
        public DbSet<Alley> Alleys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SentProducts>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}