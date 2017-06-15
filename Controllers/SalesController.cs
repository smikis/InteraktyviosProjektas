using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Digital.Data;
using Digital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Digital.Controllers
{
    [Produces("application/json")]
    [Route("api/Sales")]
    public class SalesController : Controller
    {
        private readonly ISalesRepository _context;
        private readonly IProductRepository _products;
        private readonly UserManager<ApplicationUser> _userManager;
        public SalesController(ISalesRepository context, UserManager<ApplicationUser> userManager, IProductRepository productRepo)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [Authorize("Bearer", Roles = "Administrator")]
        public IEnumerable<Sale> GetSales()
        {
            return _context.GetSales();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
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
        [Authorize("Bearer", Roles = "Administrator")]
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
        [Authorize("Bearer", Roles = "User")]
        public async Task<IActionResult> PostSales([FromBody] List<SaleProduct> products)
        {
            var userId = HttpContext.User.FindFirst("ID").Value;
            var userIdd = User.FindAll(ClaimTypes.NameIdentifier);
            var actualUser = await _userManager.FindByIdAsync(userId);            
            var sale = new Sale();

            var lines = products.GroupBy(x => x.productID).ToDictionary(y=> y.Key, y=> y.ToList());

            List<SaleLine> saleLines = new List<SaleLine>();
            foreach(var line in lines)
            {
                var saleLine = new SaleLine();
                saleLine.Product = _products.GetProductByID(int.Parse(line.Key));
                saleLine.Quantity = line.Value.Count;
                saleLines.Add(saleLine);
            }

            sale.Buyer = actualUser;
            sale.CreatedDate = DateTime.UtcNow;
            sale.TotalAmount = products.Sum(x => x.price).ToString();
            sale.TotalQuantity = products.Count.ToString();
                  
            _context.InsertSale(sale);
            _context.Save();

            return CreatedAtAction("GetProduct", new { id = sale.SaleID }, sale);
            
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