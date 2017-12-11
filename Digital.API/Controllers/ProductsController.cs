using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Digital.Contracts;
using Digital.Database.Repositories;
using Digital.API.Services;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: api/Products
        [HttpGet("{page?}/{pageSize?}")]
        public IEnumerable<Product> GetProducts(int page, int pageSize, string searchTerm)
        {
            return _productsService.GetProducts(page,pageSize,searchTerm);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productsService.GetProduct(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();
        }
        //TODO Create new controller for images
        [HttpGet("[action]/{id}")]
        public IActionResult GetProductImage(int id)
        {
            return File(_productsService.GetProductImage(id), "image/png");
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult PutProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_productsService.UpdateProduct(id, product))
            {
                return Ok(product);
            }
            
            return BadRequest();
        }

        // POST: api/Products
        [HttpPost]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult PostProduct([FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_productsService.CreateProduct(product))
            {
                return Created($"/api/Products/{product.ProductID}", product);
            }

            return BadRequest();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_productsService.DeleteProduct(id))
            {
                return new EmptyResult();
            }

            return BadRequest();
        }     


    }
}