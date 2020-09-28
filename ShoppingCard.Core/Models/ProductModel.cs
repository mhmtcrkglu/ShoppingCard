using System;

namespace ShoppingCard.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public CategoryModel Category { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}