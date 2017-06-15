using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Models
{
    public class SaleLine
    {
        public string SaleLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double LineTotal { get; set; }
    }
}
