using System;
using System.Collections.Generic;
using System.Text;

namespace Digital.Contracts
{
    public class ProductLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
