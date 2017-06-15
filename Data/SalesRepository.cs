using Digital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Data
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
            return context.Sales;
        }

        public Sale GetSaleByID(int id)
        {
            return context.Sales.Find(id);
        }


        public void InsertSale(Sale product)
        {
            context.Sales.Add(product);
        }

        public void DeleteSale(int saleID)
        {
            Sale sale = context.Sales.Find(saleID);
            context.Sales.Remove(sale);
        }

        public void UpdateSale(Sale sale)
        {
            context.Entry(sale).State = EntityState.Modified;
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
