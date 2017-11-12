using System.Collections.Generic;
using System.Threading.Tasks;
using Digital.Contracts;

namespace Digital.API.Services
{
    public interface ISalesService
    {
        IEnumerable<Sale> GetSales(int page, int pageSize);
        Sale GetSale(int id);
        bool UpdateSale(int id, Sale sale);
        Task<bool> CreateSaleAsync(List<Product> products, string userId);
        bool DeleteSale(int id);
    }
}