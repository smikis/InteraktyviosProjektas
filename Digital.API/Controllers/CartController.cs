using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Digital.API.Models;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        // GET: api/Cart
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return new Cart[] { new Cart { Id = "New"} };
        }

        // GET: api/Cart/5
        [HttpGet("{id}", Name = "Get")]
        public Cart Get(string id)
        {
            return new Cart { Id = "New get one" };
        }
        
        // POST: api/Cart
        [HttpPost]
        public StatusCodeResult Post([FromBody]Cart value)
        {
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
        
        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public StatusCodeResult Put(string id, [FromBody]Cart value)
        {
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
        
        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(string id)
        {
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}
