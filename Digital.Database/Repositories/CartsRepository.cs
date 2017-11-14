using System;
using System.Collections.Generic;
using System.Linq;
using Digital.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Digital.Database.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly ApplicationDbContext _context;
        public CartsRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return _context.Carts.Include("ProductLines");
        }


        public Cart GetCartById(int id)
        {
            return _context.Carts.Find(id);
        }

        public void InsertCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(int cartId)
        {
            Cart cart = _context.Carts.Include("ProductLines").SingleOrDefault(x => x.Id == cartId);
            if (cart.ProductLines != null && cart.ProductLines.Any())
            {
                foreach (var salesLine in cart.ProductLines)
                {
                    _context.SaleLines.Remove(salesLine);
                }
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart product)
        {
            Cart cart = _context.Carts.SingleOrDefault(x => x.Id == product.Id);
            cart.ModifyDate = product.ModifyDate;
            if (product.ProductLines != null && product.ProductLines.Any())
            {
                foreach (var salesLine in product.ProductLines)
                {      
                    _context.SaleLines.Update(salesLine);
                }
            }
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
