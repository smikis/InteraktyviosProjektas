using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Digital.API.Services;
using Digital.Database.Repositories;
using Digital.Contracts;

namespace Digital.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Sales")]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // GET: api/Products
        [HttpGet("{page?}/{pageSize?}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult GetSales(int page, int pageSize)
        {
            return Ok(_salesService.GetSales(page,pageSize));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult Getsale(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sale = _salesService.GetSale(id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }


        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult PutSale(int id, [FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_salesService.UpdateSale(id, sale))
            {
                return Ok();
            }

            return BadRequest();
        }

        // POST: api/Products
        [HttpPost]
        [Authorize("Bearer", Roles = "User")]
        public async Task<IActionResult> PostSale([FromBody] List<Product> products)
        {
            var userId = HttpContext.User.FindFirst("ID").Value;
            if (await _salesService.CreateSaleAsync(products, userId))
            {
                return Ok();
            }

            return BadRequest();         
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_salesService.DeleteSale(id))
            {
                return new EmptyResult();
            }

            return BadRequest();
        }
    }
}