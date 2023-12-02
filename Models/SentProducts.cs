namespace WarehouseMenagementAPI.Models
{
    public class SentProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string PostalCode { get; set; }
        public int AlleyId { get; set; }
        public DateTime SentDate { get; set; }
    }
}
