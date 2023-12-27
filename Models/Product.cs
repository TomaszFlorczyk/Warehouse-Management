using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseMenagementAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? PostalCode { get; set; }
        public Alley Alley { get; set; } = null!;
    }
}