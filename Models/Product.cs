﻿namespace WarehouseMenagementAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PostalCode { get; set; }

        public int AlleyId { get; set; }
        public Alley Alley { get; set; }
    }
}