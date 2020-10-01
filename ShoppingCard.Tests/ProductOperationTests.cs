using System;
using ShoppingCard.Core.Models;
using ShoppingCard.Core.Operations;
using Xunit;

namespace ShoppingCard.Tests
{
    public class ProductOperationTests
    {
        private readonly ProductOperations _productOperations = new ProductOperations();

        [Fact]
        public void GetProductById_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var title = "Playstation 5";
            var price = 8499;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = new CategoryModel(),
                Price = price
            };
            
            ProductOperations.ProductList.Add(productDto);
            
            //Act
            var product = _productOperations.GetProduct(productId);
            
            //Assert
            Assert.Equal(productId,product.Id);
        }
        
        [Fact]
        public void GetProductById_ShouldReturnProduct_WhenProductDoesNotExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var title = "Playstation 5";
            var price = 8499;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = new CategoryModel(),
                Price = price
            };
            
            ProductOperations.ProductList.Add(productDto);
            
            //Act
            var product = _productOperations.GetProduct(Guid.NewGuid());
            
            //Assert
            Assert.Null(product);
        }
        
                [Fact]
        public void AddProduct_ShouldReturnProductModel_WhenProductAddedSuccess()
        {
            //Arrange
            var title = "Playstation 5";
            var price = 8499;
            //Act
            var result = _productOperations.AddProduct(title,price);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void AddProduct_ShouldReturnNull_WhenProductAddedFail()
        {
            //Arrange
            var title = string.Empty;
            var price = 8499;
            //Act
            var result = _productOperations.AddProduct(title,price);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void RemoveProduct_ShouldReturnTrue_WhenProductExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Technology";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = new CategoryModel(),
            };
            ProductOperations.ProductList.Add(productDto);

            //Act
            var result = _productOperations.RemoveProduct(productDto.Id);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void RemoveProduct_ShouldReturnFalse_WhenProductNotExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Technology";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = new CategoryModel(),
            };
            ProductOperations.ProductList.Add(productDto);

            //Act
            var result = _productOperations.RemoveProduct(Guid.NewGuid());

            //Assert
            Assert.False(result);
        }
        
        [Fact]
        public void BindProduct_ShouldReturnTrue_WhenParentProductExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle
            };
            ProductOperations.ProductList.Add(productDto);
            
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle,
                ParentCategory = new CategoryModel()
            };
            ProductOperations.ProductList.Add(productDto);

            //Act
            var result = _productOperations.BindCategory(productId,categoryDto);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BindProduct_ShouldReturnFalse_WhenParentProductNotExists()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle
            };
            ProductOperations.ProductList.Add(productDto);

            //Act
            var result = _productOperations.BindCategory(productId,new CategoryModel());

            //Assert
            Assert.False(result);
        }
    }
}