
using Digital.Models;
using Digital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("[action]")]
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

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {                 
                    _logger.LogInformation(1, "Logged in. Generating token");
                    var user = await _userManager.FindByEmailAsync(model.Email);              
                    var token = await _authenticationService.GetAuthorizationToken(user);
                    
                    return Ok(token);
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return BadRequest(GetErrors());
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
