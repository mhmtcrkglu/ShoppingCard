using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CategoryOperations : ICategoryOperations
    {
        public Settings _settings;
        public static List<CategoryModel> _categoryList;
        
        public CategoryOperations(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        public CategoryModel GetCategory(Guid categoryId)
        {
            return _categoryList?.FirstOrDefault(a => a.Id == categoryId);
        }

        public Guid AddCategory(string title)
        {
            var category = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Title = title
            };
            _categoryList.Add(category);

            return category.Id;
        }

        public bool RemoveCategory(Guid categoryId)
        {
            var category = GetCategory(categoryId);

            if (category != null)
            {
                _categoryList.Remove(category);
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

            return false;
        }
    }
}