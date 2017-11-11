using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Contracts
{
    public class Cart
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public List<SaleLine> ProductLines {get;set;}
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
