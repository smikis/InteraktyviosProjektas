using Microsoft.AspNetCore.Identity;

namespace Digital.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
