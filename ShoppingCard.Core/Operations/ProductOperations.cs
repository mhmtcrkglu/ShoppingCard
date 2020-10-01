using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class ProductOperations : IProductOperations
    {
        public static readonly List<ProductModel> ProductList = new List<ProductModel>();

        public ProductModel GetProduct(Guid productId)
        {
            return ProductList.FirstOrDefault(a => a.Id == productId);
        }
        public ProductModel AddProduct(string title, decimal price)
        {
            if (!string.IsNullOrEmpty(title))
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
            Console.WriteLine("Ürün ismi boş geçilemez.");
            return null;

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

            if (product != null && category != null && category.Id != Guid.Empty)
            {
                product.Category = category;
                return true;
            }
            Console.WriteLine("Ürün - kategori boş geçilemez");
            return false;
        }
    }
}