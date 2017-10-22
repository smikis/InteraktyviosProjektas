using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.API.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Image { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public Category Category { get; set; }
    }
}
