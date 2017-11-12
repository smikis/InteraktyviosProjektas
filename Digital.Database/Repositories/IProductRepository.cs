using System.Collections.Generic;
using System;
using Digital.Contracts;

namespace Digital.Database.Repositories
{
    public interface IProductRepository : IDisposable
    {
        void DeleteProduct(int productId);
        Product GetProductByID(int id);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product product);
        void Save();
        void UpdateProduct(Product product);
        byte[] GetProductImage(int id);
        IEnumerable<Product> GetProductsWithouImages();
        IEnumerable<Product> GetProductsPage(int page, int pageSize, string searchTerm, Category category);
    }
}