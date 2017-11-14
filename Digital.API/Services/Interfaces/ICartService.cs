using System.Collections.Generic;
using Digital.Contracts;

namespace Digital.API.Services
{
    public interface ICartService
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCart(int id);
        bool UpdateCart(int id, Cart cart);
        bool InsertCart(Cart cart);
        bool DeleteCart(int id);
        bool CartExists(int id);
    }
}