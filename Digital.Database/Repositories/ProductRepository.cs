using Digital.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Digital.Database.Repositories
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        public IEnumerable<Product> GetProductsWithouImages()
        {
            var products = context.Products.ToList();
            products.ForEach(x => x.Image = null);
            return products;
        }

        public IEnumerable<Product> GetProductsPage(int page, int pageSize, string searchTerm, Category category)
        {
            IQueryable<Product> products = context.Products;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.Contains(searchTerm) || x.Description.Contains(searchTerm));
            }

            if (category != null)
            {
                products = products.Where(x => x.Category.CategoryID == category.CategoryID);
            }
            var productsResult = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            productsResult.ForEach(x => x.Image = null);
            return productsResult;
        }
       


        public Product GetProductByID(int id)
        {
            return context.Products.Find(id);
        }

        public byte[] GetProductImage(int id)
        {
            return context.Products.Find(id)?.Image;
        }

        public void InsertProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product product = context.Products.Find(productId);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
