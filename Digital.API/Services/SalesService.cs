using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Digital.Contracts;
using Digital.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Digital.API.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _context;
        private readonly IProductRepository _products;
        private readonly UserManager<ApplicationUser> _userManager;
        public SalesService(ISalesRepository context, UserManager<ApplicationUser> userManager, IProductRepository productRepo)
        {
            _products = productRepo;
            _userManager = userManager;
            _context = context;
        }

        public IEnumerable<Sale> GetSales(int page, int pageSize)
        {
            if (page == 0 || pageSize == 0)
            {
                return _context.GetSales();
            }
            return _context.GetSalesPage(page, pageSize);
        }

        public Sale GetSale(int id)
        {
            return _context.GetSaleByID(id);
        }

        public bool UpdateSale(int id, Sale sale)
        {
            if (id != sale.SaleID)
            {
                return false;
            }
            try
            {
                var actualSale = _context.GetSaleByID(sale.SaleID);
                actualSale.TotalAmount = sale.TotalAmount;
                actualSale.TotalQuantity = sale.TotalQuantity;
                _context.UpdateSale(actualSale);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public async Task<bool> CreateSaleAsync(List<Product> products, string userId)
        {        
            var user = await _userManager.FindByIdAsync(userId);
            var sale = new Sale();       

            List<SaleLine> saleLines = new List<SaleLine>();
            foreach (var product in products)
            {
                //TODO Fix product adding
                var saleLine = new SaleLine
                {
                    Product = _products.GetProductByID(product.ProductID),
                    Quantity = product.Quantity,
                    LineTotal = product.Quantity * product.Price
                };
                saleLines.Add(saleLine);
            }

            sale.Buyer = user;
            sale.CreatedDate = DateTime.UtcNow;
            sale.TotalAmount = saleLines.Sum(x => x.LineTotal).ToString(CultureInfo.InvariantCulture);
            sale.TotalQuantity = saleLines.Sum(x => x.Quantity).ToString();
            sale.PurchaseList = saleLines;
            try
            {
                _context.InsertSale(sale);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

        public bool DeleteSale(int id)
        {
            var sale = _context.GetSaleByID(id);
            if (sale == null)
            {
                return false;
            }

          
            try
            {
                _context.DeleteSale(sale.SaleID);
            }
            catch (DbException e)
            {
                //TODO Add logging
                return false;
            }
            return true;
        }

    }
}
