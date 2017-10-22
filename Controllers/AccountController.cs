
using Digital.Models;
using Digital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IRolesService _rolesService;
        private readonly ILogger _logger;
        public AccountController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory, SignInManager<ApplicationUser> signInManager, IAuthenticationService authenticationService, IRolesService rolesService)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _rolesService = rolesService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register model)
        {          
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                 
                    _logger.LogInformation(3, "User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "User");
                    return Ok();
                }
                GetErrors(result);
            }
            return BadRequest(GetErrors());
        }

        [HttpGet]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult GetAllUsers()
        {
            var result =  _userManager.Users.Include(u => u.Roles).Select(x => new { Email = x.Email, Name = x.Name, LastName = x.LastName, ID = x.Id }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult GetUser()
        {
            var result = _userManager.Users.Include(u => u.Roles).Select(x => new { Email = x.Email, Name = x.Name, LastName = x.LastName, ID = x.Id }).ToList().First();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult UpdateUser()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer", Roles = "Administrator")]
        public async Task<ActionResult> RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
             var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRoles()
        {
            await _rolesService.GenerateRoles();
            return Ok("Roles created");
        }

        [HttpGet("[action]")]
        [Authorize("Bearer", Roles = "Administrator")]
        public IActionResult TestAdmin()
        {
            _rolesService.GenerateRoles();
            return Ok("Roles created");
        }

        [HttpGet("[action]")]
        [Authorize("Bearer", Roles = "User")]
        public IActionResult TestUser()
        {
            _rolesService.GenerateRoles();
            return Ok("Roles created");
        }
   

        private List<string> GetErrors()
        {
            var list = new List<string>();
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    if(!string.IsNullOrEmpty(error.ErrorMessage))
                    list.Add(error.ErrorMessage);
                }              
            }
            return list;
        }

        private void GetErrors(IdentityResult result)
        {           
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
