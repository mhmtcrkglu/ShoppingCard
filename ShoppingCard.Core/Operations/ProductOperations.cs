using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class ProductOperations : IProductOperations
    {
        private static readonly List<ProductModel> ProductList = new List<ProductModel>();

        public ProductModel GetProduct(Guid productId)
        {
            return ProductList.FirstOrDefault(a => a.Id == productId);
        }
        public ProductModel AddProduct(string title, decimal price)
        {
            var product = new ProductModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                Price = price
            };
            ProductList.Add(product);

            return product;
        }
        public bool RemoveProduct(Guid productId)
        {
            var product = GetProduct(productId);

            if (product != null)
            {
                ProductList.Remove(product);
                return true;
            }

            return false;
        }
        public bool BindCategory(Guid productId, CategoryModel category)
        {
            var product = GetProduct(productId);

            if (product != null)
            {
                product.Category = category;
                return true;
            }
            Console.WriteLine("Product cannot be found");
            return false;
        }
    }
}