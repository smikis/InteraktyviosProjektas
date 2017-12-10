using System.Collections.Generic;
using Digital.Contracts;
using Digital.Contracts.ViewModels;

namespace Digital.API.Services
{
    public interface ICartService
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCart(int id);
        bool UpdateCart(int id, CartModel cart);
        int InsertCart(CartModel cart);
        bool DeleteCart(int id);
        bool CartExists(int id);
    }
}