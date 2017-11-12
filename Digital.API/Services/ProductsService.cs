using Digital.Contracts;
using Digital.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Digital.API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _context;

        public ProductsService(IProductRepository context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts(int page, int pageSize, string searchTerm)
        {
            if (page == 0 || pageSize == 0)
            {
                return _context.GetProductsWithouImages();
            }
            return _context.GetProductsPage(page, pageSize, searchTerm, null);
        }

        public Product GetProduct(int id)
        {
            return _context.GetProductByID(id);
        }

        public byte[] GetProductImage(int id)
        {
            var image = _context.GetProductImage(id);
            return image ?? File.ReadAllBytes("images.jpg"); //TODO Specify real path
        }

        public bool UpdateProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return false;
            }
            try
            {
                //TODO Fix update to fetch object before updating
                _context.UpdateProduct(product);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool CreateProduct(Product product, IFormFile file)
        {
            product.CreateDate = DateTime.UtcNow;
            if (file != null)
            {
                product.Image = GetImageBytes(file);
            }
            try
            {
                _context.InsertProduct(product);
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
            var product = _context.GetProductByID(id);
            if (product == null)
            {
                return false;
            }
            try
            {
                _context.DeleteProduct(product.ProductID);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        private byte[] GetImageBytes(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }

    }
}
