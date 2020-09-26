using System;

namespace ShoppingCard.Core.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public CategoryModel ParentCategory { get; set; }
    }
}