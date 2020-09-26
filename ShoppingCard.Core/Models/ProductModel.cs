using System;

namespace ShoppingCard.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}