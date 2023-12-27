namespace WarehouseMenagementAPI.Models
{
    public class Alley
    {
        public int Id { get; set; }
        public int AlleyId { get; set; }
        public string? Name { get; set; }

        public Warehouse? Warehouse { get; set; }
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
} 