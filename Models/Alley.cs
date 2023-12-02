namespace WarehouseMenagementAPI.Models
{
    public class Alley
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}