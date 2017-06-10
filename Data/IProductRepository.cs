using System.Collections.Generic;
using Digital.Models;

namespace Digital.Data
{
    public interface IProductRepository
    {
        void DeleteProduct(int productID);
        Product GetProductByID(int id);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product product);
        void Save();
        void UpdateProduct(Product product);
    }
}