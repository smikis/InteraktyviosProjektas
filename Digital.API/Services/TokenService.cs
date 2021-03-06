﻿using Digital.API.Helpers;
using Digital.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Digital.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetAuthorizationToken(ApplicationUser user)
        {
            var requestAt = DateTime.Now;
            var expiresIn = requestAt + TimeSpan.FromMinutes(60);
            var token = await GenerateToken(user, expiresIn);

            return JsonConvert.SerializeObject(new {
                requestAt = requestAt,
                expiresIn = expiresIn,
                tokenType = AuthenticationOptions.TokenType,
                accessToken = token,
                isAdmin = await _userManager.IsInRoleAsync(user, "Administrator")
            }
            );
        }


    
        private async Task<string> GenerateToken(ApplicationUser user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>();
            foreach (var item in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", item));
            }
            claims.Add(new Claim("ID", user.Id.ToString()));
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "TokenAuth"),
                claims.ToArray()
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = AuthenticationOptions.Issuer,
                Audience = AuthenticationOptions.Audience,
                SigningCredentials = AuthenticationOptions.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }




    }


   
}
