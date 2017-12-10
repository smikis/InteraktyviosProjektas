using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Digital.Contracts;
using Digital.Contracts.ViewModels;
using Digital.Database.Repositories;

namespace Digital.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartsRepository _context;
        private IProductRepository _productRepo;

        public CartService(ICartsRepository context, IProductRepository productRepo)
        {
            _context = context;
            _productRepo = productRepo;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return _context.GetCarts();
        }

        public Cart GetCart(int id)
        {
            try
            {
                return _context.GetCartById(id);
            }
            catch (DbException e)
            {
                //TODO Add Exception handling
                return null;
            }
        }

        public bool UpdateCart(int id, CartModel cartModel)
        {      
            try
            {
                var oldCart = _context.GetCartById(id);
                oldCart.ModifyDate = DateTime.Now;

                var productLines = cartModel.ProductLines.Select(x =>
                    new ProductLine {Quantity = x.Quantity, Product = _productRepo.GetProductByID(x.Product)});

                oldCart.ProductLines = productLines.ToList();
                _context.UpdateCart(oldCart);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public int InsertCart(CartModel cartModel)
        {
            var cart = new Cart
            {
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };

            
            try
            {
                var productLines = cartModel.ProductLines.Select(x =>
                    new ProductLine { Quantity = x.Quantity, Product = _productRepo.GetProductByID(x.Product) });
                cart.ProductLines = productLines.ToList();
                   return  _context.InsertCart(cart);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return -1;
            }
        }

        public bool DeleteCart(int id)
        {            
            try
            {
                _context.DeleteCart(id);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool CartExists(int id)
        {
            var cart = _context.GetCartById(id);
            if (cart == null)
            {
                return false;
            }
            return true;
        }
    }
}
