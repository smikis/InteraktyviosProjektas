using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital.API.Services;
using Digital.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        // GET: api/Cart
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoriesService.GetCategories();
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoriesService.GetCategory(id);
        }

        // POST: api/Cart
        [HttpPost]
        public IActionResult Post([FromBody]Category value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_categoriesService.CreateCategory(value))
            {
                return Created($"/api/categories/{value.CategoryID}", value);
            }
            return BadRequest();
        }

        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_categoriesService.UpdateCategory(id, value))
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (_categoriesService.DeleteProduct(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}