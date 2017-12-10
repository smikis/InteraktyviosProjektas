using System.Collections.Generic;
using Digital.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Digital.Contracts;
using Digital.Contracts.ViewModels;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        // GET: api/Cart
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _cartService.GetCarts();
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cart = _cartService.GetCart(id);
            if (cart != null)
            {
                return Ok(cart);
            }
            return NotFound();
        }
        
        // POST: api/Cart
        [HttpPost]
        public IActionResult Post([FromBody]CartModel value)
        {
            var id = _cartService.InsertCart(value);
            if (id != -1 )
            {
                return Created($"/api/carts{id}",id);
            }
            return BadRequest();
        }
        
        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public StatusCodeResult Put(int id, [FromBody]CartModel value)
        {
            if (!_cartService.CartExists(id))
            {
                return NotFound();
            }
            if (_cartService.UpdateCart(id, value))
            {
                return Ok();
            }
            return BadRequest();
        }
        
        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (!_cartService.CartExists(id))
            {
                return NotFound();
            }
            if (_cartService.DeleteCart(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
