using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Contracts
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public List<ProductLine> ProductLines {get;set;}
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
