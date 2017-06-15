using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Digital.Data;
using Digital.Models;
using Microsoft.EntityFrameworkCore;

namespace Digital.Controllers
{
    [Produces("application/json")]
    [Route("api/Sales")]
    public class SalesController : Controller
    {
        private readonly ISalesRepository _context;

        public SalesController(ISalesRepository context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Sale> GetSales()
        {
            return _context.GetSales();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult Getsale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sale = _context.GetSaleByID(id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }


        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult PutSale([FromRoute] int id, Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.SaleID)
            {
                return BadRequest();
            }

            _context.UpdateSale(sale);

            try
            {
                _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost("[action]")]
        public IActionResult PostSales(List<SaleProduct> products)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sale.CreatedDate = DateTime.UtcNow;          
            _context.InsertSale(sale);
            _context.Save();

            return CreatedAtAction("GetProduct", new { id = sale.SaleID }, sale);
            */
            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sale = _context.GetSaleByID(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.DeleteSale(sale.SaleID);
            _context.Save();

            return Ok(sale);
        }
    }
}