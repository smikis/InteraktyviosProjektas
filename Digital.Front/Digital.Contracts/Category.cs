using System.ComponentModel.DataAnnotations;

namespace Digital.Contracts
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
