using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digital.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Digital.Database.Repositories
{
    public class CategoriesRepository : IDisposable, ICategoriesRepository
    {
        private readonly ApplicationDbContext context;
        public CategoriesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories;
        }


        public Category GetCategoryById(int id)
        {
            return context.Categories.Find(id);
        }

        public void InsertCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeteleCategory(int productId)
        {
            Category product = context.Categories.Find(productId);
            context.Categories.Remove(product);
            context.SaveChanges();
        }

        public void UpdateCategory(Category product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
