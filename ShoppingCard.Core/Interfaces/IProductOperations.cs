using System;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Interfaces
{
    public interface IProductOperations
    {
        public ProductModel GetProduct(Guid productId);
        public ProductModel AddProduct(string title, decimal price);
        public bool RemoveProduct(Guid productId);
        public bool BindCategory(Guid productId, CategoryModel category);
    }
}