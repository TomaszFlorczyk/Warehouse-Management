namespace WarehouseMenagementAPI.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }

        public List<Alley> Alleys { get; set; }
    }
}