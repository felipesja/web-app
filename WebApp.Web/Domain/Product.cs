﻿namespace WebApp.Web.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Sku { get; set; }
        public decimal Price { get; set; }
    }
}
