using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CategoryOperations : ICategoryOperations
    {
        private static readonly List<CategoryModel> CategoryList = new List<CategoryModel>();
        
        public CategoryModel GetCategory(Guid categoryId)
        {
            return CategoryList?.FirstOrDefault(a => a.Id == categoryId);
        }
        public CategoryModel AddCategory(string title)
        {
            var category = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Title = title
            };
            CategoryList.Add(category);

            return category;
        }
        public bool RemoveCategory(Guid categoryId)
        {
            var category = GetCategory(categoryId);

            if (category != null)
            {
                CategoryList.Remove(category);
                return true;
            }

            return false;
        }
        public bool BindCategory(Guid categoryId, Guid parentCategoryId)
        {
            var category = GetCategory(categoryId);
            var parentCategory = GetCategory(parentCategoryId);

            if (category != null && parentCategory != null)
            {
                category.ParentCategory = parentCategory;
                return true;
            }
            Console.WriteLine("Category cannot be found");
            return false;
        }
    }
}