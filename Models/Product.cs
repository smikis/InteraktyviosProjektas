using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Image { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public Category Category { get; set; }
    }
}
