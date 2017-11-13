using System;
using System.Collections.Generic;
using Digital.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Digital.Database.Repositories
{
    public class CartsRepository
    {
        private readonly ApplicationDbContext _context;
        public CartsRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return _context.Carts;
        }


        public Cart GetCartById(string id)
        {
            return _context.Carts.Find(id);
        }

        public void InsertCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(string cartId)
        {
            Cart cart = _context.Carts.Find(cartId);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void UpdateCategory(Cart product)
        {
            _context.Entry(product).State = EntityState.Modified;
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
