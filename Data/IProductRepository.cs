using System.Collections.Generic;
using Digital.Models;
using System;

namespace Digital.Data
{
    public interface IProductRepository : IDisposable
    {
        void DeleteProduct(int productID);
        Product GetProductByID(int id);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product product);
        void Save();
        void UpdateProduct(Product product);
        byte[] GetProductImage(int id);
        IEnumerable<Product> GetProductsWithouImages();
    }
}