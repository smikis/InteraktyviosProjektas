using System.Collections.Generic;
using Digital.Contracts;
using Microsoft.AspNetCore.Http;

namespace Digital.API.Services
{
    public interface IProductsService
    {
        Product GetProduct(int id);
        byte[] GetProductImage(int id);
        bool UpdateProduct(int id, Product product);
        bool CreateProduct(Product product);
        bool DeleteProduct(int id);
        IEnumerable<Product> GetProducts(int page, int pageSize, string searchTerm);
    }
}