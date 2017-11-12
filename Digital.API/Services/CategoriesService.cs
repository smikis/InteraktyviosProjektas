using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Digital.Contracts;
using Digital.Database.Repositories;
using Microsoft.AspNetCore.Http;

namespace Digital.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _context;

        public CategoriesService(ICategoriesRepository context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.GetCategories();
        }

        public Category GetCategory(int id)
        {
            return _context.GetCategoryById(id);
        }

        public bool UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return false;
            }
            try
            {
                _context.UpdateCategory(category);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool CreateCategory(Category category)
        {         
            try
            {
                _context.InsertCategory(category);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var category = _context.GetCategoryById(id);
            if (category == null)
            {
                return false;
            }
            try
            {
                _context.DeteleCategory(category.CategoryID);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }
    }
}
