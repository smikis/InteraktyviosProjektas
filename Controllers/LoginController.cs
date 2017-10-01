using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Digital.Models;
using Microsoft.AspNetCore.Identity;
using Digital.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Digital.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger _logger;
        public LoginController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory, SignInManager<ApplicationUser> signInManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        [HttpPost]
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

        [HttpPut]
        public ActionResult RefreshToken(string id)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult RemoveToken(string id)
        {
            return Ok();
        }

        private List<string> GetErrors()
        {
            var list = new List<string>();
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                        list.Add(error.ErrorMessage);
                }
            }
            return list;
        }
    }
}