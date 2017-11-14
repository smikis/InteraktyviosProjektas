using System;
using System.Collections.Generic;
using System.Data.Common;
using Digital.Contracts;
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

        public bool UpdateCart(int id, Cart cart)
        {
            cart.ModifyDate = DateTime.UtcNow;           
            if (id != cart.Id)
            {
                return false;
            }
            try
            {
                var oldCart = _context.GetCartById(cart.Id);
                oldCart.ProductLines = cart.ProductLines;
                _context.UpdateCart(cart);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool InsertCart(Cart cart)
        {
            cart.CreateDate = DateTime.UtcNow;
            cart.ModifyDate = DateTime.UtcNow;
            try
            {               
                _context.InsertCart(cart);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
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
