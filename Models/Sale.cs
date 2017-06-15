using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUser Buyer { get; set; }
        public List<SaleLine> PurchaseList { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQuantity { get; set; }
    }
}
