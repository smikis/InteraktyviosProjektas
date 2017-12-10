using System.Collections.Generic;
using Digital.Contracts;

namespace Digital.Database.Repositories
{
    public interface ICartsRepository
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCartById(int id);
        int InsertCart(Cart cart);
        void DeleteCart(int cartId);
        void UpdateCart(Cart product);
        void Dispose();
    }
}