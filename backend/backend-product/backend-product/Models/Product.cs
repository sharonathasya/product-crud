﻿using System.ComponentModel.DataAnnotations;

namespace backend_product.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
