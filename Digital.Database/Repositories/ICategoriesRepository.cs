using System.Collections.Generic;
using Digital.Contracts;

namespace Digital.Database.Repositories
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        void InsertCategory(Category category);
        void DeteleCategory(int productId);
        void UpdateCategory(Category product);
        void Dispose();
    }
}