using Digital.Contracts;
using System.Collections.Generic;

namespace Digital.Database.Repositories
{
    public interface ISalesRepository
    {
        void DeleteSale(int saleID);
        void Dispose();
        Sale GetSaleByID(int id);
        IEnumerable<Sale> GetSales();
        void InsertSale(Sale product);
        void Save();
        void UpdateSale(Sale sale);
        IEnumerable<Sale> GetSalesPage(int page, int pageSize);
    }
}