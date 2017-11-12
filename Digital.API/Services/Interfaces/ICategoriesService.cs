using System.Collections.Generic;
using Digital.Contracts;

namespace Digital.API.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        bool UpdateCategory(int id, Category category);
        bool CreateCategory(Category category);
        bool DeleteProduct(int id);
    }
}