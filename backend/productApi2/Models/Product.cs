﻿using System.Diagnostics.CodeAnalysis;

namespace ProductApi2.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; } 

        public decimal Price { get; set; }
    }
}
