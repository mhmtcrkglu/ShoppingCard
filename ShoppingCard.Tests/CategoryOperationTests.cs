using System;
using ShoppingCard.Core.Models;
using ShoppingCard.Core.Operations;
using Xunit;

namespace ShoppingCard.Tests
{
    public class CategoryOperationTests
    {
        private readonly CategoryOperations _categoryOperations = new CategoryOperations();

        [Fact]
        public void GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle,
                ParentCategory = new CategoryModel()
            };
            CategoryOperations.CategoryList.Add(categoryDto);

            //Act
            var category = _categoryOperations.GetCategory(categoryId);

            //Assert
            Assert.Equal(categoryId, category.Id);
        }

        [Fact]
        public void GetCategoryById_ShouldReturnCategory_WhenCategoryDoesNotExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle,
                ParentCategory = new CategoryModel()
            };
            CategoryOperations.CategoryList.Add(categoryDto);

            //Act
            var category = _categoryOperations.GetCategory(Guid.NewGuid());

            //Assert
            Assert.Null(category);
        }

        [Fact]
        public void AddCategory_ShouldReturnCategoryModel_WhenCategoryAddedSuccess()
        {
            //Arrange
            var categoryTitle = "Technology";

            //Act
            var result = _categoryOperations.AddCategory(categoryTitle);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void AddCategory_ShouldReturnNull_WhenCategoryAddedFail()
        {
            //Arrange
            var categoryTitle = String.Empty;

            //Act
            var result = _categoryOperations.AddCategory(categoryTitle);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void RemoveCategory_ShouldReturnTrue_WhenCategoryExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle,
                ParentCategory = new CategoryModel()
            };
            CategoryOperations.CategoryList.Add(categoryDto);

            //Act
            var result = _categoryOperations.RemoveCategory(categoryDto.Id);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void RemoveCategory_ShouldReturnFalse_WhenCategoryNotExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle,
                ParentCategory = new CategoryModel()
            };
            CategoryOperations.CategoryList.Add(categoryDto);

            //Act
            var result = _categoryOperations.RemoveCategory(Guid.NewGuid());

            //Assert
            Assert.False(result);
        }
        
        [Fact]
        public void BindCategory_ShouldReturnTrue_WhenParentCategoryExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Mobile Phone";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };
            CategoryOperations.CategoryList.Add(categoryDto);
            
            var parentCategoryId = Guid.NewGuid();
            var parentCategoryTitle = "Technology";
            var parentCategoryDto = new CategoryModel
            {
                Id = parentCategoryId,
                Title = parentCategoryTitle
            };
            CategoryOperations.CategoryList.Add(parentCategoryDto);

            //Act
            var result = _categoryOperations.BindCategory(categoryId,parentCategoryId);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BindCategory_ShouldReturnFalse_WhenParentCategoryNotExists()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };
            CategoryOperations.CategoryList.Add(categoryDto);

            //Act
            var result = _categoryOperations.BindCategory(categoryId,Guid.NewGuid());

            //Assert
            Assert.False(result);
        }
    }
}