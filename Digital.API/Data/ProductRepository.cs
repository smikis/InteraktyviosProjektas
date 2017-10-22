using Digital.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Digital.API.Data
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
        }

        public void DeleteProduct(int productID)
        {
            Product Product = context.Products.Find(productID);
            context.Products.Remove(Product);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

        public void Save()
        {
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
