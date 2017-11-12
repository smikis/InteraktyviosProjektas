using Digital.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Digital.Database.Repositories
{
    public class SalesRepository : IDisposable, ISalesRepository
    {
        private ApplicationDbContext context;
        public SalesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Sale> GetSales()
        {
            return context.Sales.Include("Buyer");
        }

        public IEnumerable<Sale> GetSalesPage(int page, int pageSize)
        {
            return context.Sales.Include("Buyer").Include("PurchaseList").Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Sale GetSaleByID(int id)
        {
            return context.Sales.Find(id);
        }


        public void InsertSale(Sale product)
        {
            context.Sales.Add(product);
            context.SaveChanges();
        }

        public void DeleteSale(int saleID)
        {
            Sale sale = context.Sales.Include("PurchaseList").SingleOrDefault(x=> x.SaleID == saleID);
            if (sale.PurchaseList != null && sale.PurchaseList.Any())
            {
                foreach (var salesLine in sale.PurchaseList)
                {
                    context.SaleLines.Remove(salesLine);
                }
            }          
            context.Sales.Remove(sale);
            context.SaveChanges();
        }

        public void UpdateSale(Sale sale)
        {
            context.Entry(sale).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
