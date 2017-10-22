using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Digital.API.Data;
using Digital.API.Models;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _context;

        public ProductsController(IProductRepository context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.GetProductsWithouImages();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _context.GetProductByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                name = product.Name,
                description = product.Description,
                price = product.Price,
                quantity = product.Quantity
            }
            );
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetProductImage([FromRoute] int id)
        {
            var image = _context.GetProductImage(id);
            if(image != null)
            {
                return File(image, "image/png");
            }
            return File(System.IO.File.ReadAllBytes("ClientApp/Images/images.jpg"), "image/png");

        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult PutProduct([FromRoute] int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.UpdateProduct(product);

            try
            {
                _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        // POST: api/Products
        [HttpPost]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult PostProduct([FromBody]Product product, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.CreateDate = DateTime.UtcNow;
            product.Image = GetImageBytes(file);
            _context.InsertProduct(product);
            _context.Save();

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
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

            var product = _context.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.DeleteProduct(product.ProductID);
            _context.Save();

            return Ok(product);
        }

        private byte[] GetImageBytes(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }


    }
}