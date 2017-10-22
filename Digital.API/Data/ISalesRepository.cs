using System.Collections.Generic;
using Digital.API.Models;

namespace Digital.API.Data
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
    }
}