using Digital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task GenerateRoles()
        {
            var check = await _roleManager.RoleExistsAsync("User");
            if (!check)
            {
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);
            }
            check = await _roleManager.RoleExistsAsync("Administrator");
            if (!check)
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                await _roleManager.CreateAsync(role);

                

            }

            //Create administrator user
            var user = new ApplicationUser();
            user.UserName = "admin@admin.com";
            user.Email = "admin@admin.com";
            
            string userPWD = "Naujas11!";

            IdentityResult result = await _userManager.CreateAsync(user, userPWD);

            if (result.Succeeded)
            {
            var result1 = _userManager.AddToRoleAsync(user, "Administrator");
                var result2 = _userManager.AddToRoleAsync(user, "User");
            }

        }
    }
}
