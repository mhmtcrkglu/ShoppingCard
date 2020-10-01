using System;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Interfaces
{
    public interface ICategoryOperations
    {
        public CategoryModel GetCategory(Guid categoryId);
        public CategoryModel AddCategory(string title);
        public bool RemoveCategory(Guid categoryId);
        public bool BindCategoryToCategory(Guid categoryId, Guid parentCategoryId);
    }
}