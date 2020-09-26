using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class ProductOperations : IProductOperations
    {
        public static List<ProductModel> _productList;

        public ProductOperations()
        {
        }

        public ProductModel GetProduct(Guid productId)
        {
            return _productList.FirstOrDefault(a => a.Id == productId);
        }

        public Guid AddProduct(string title, decimal price)
        {
            var product = new ProductModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                Price = price
            };
            _productList.Add(product);

            return product.Id;
        }

        public bool RemoveProduct(Guid productId)
        {
            var product = GetProduct(productId);

            if (product != null)
            {
                _productList.Remove(product);
                return true;
            }

            return false;
        }


        public bool BindCategory(Guid productId, Guid categoryId)
        {
            var product = GetProduct(productId);

            if (product != null)
            {
                product.CategoryId = categoryId;
                return true;
            }

            return false;
        }
    }
}