using Digital.Helpers;
using Digital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Digital.Services
{
    public class AuthenticationService : IAuthenticationService
    {
       

        public string GetAuthorizationToken(ApplicationUser user)
        {
            var requestAt = DateTime.Now;
            var expiresIn = requestAt + TimeSpan.FromMinutes(60);
            var token = GenerateToken(user, expiresIn);

            return JsonConvert.SerializeObject(new {
                requestAt = requestAt,
                expiresIn = expiresIn,
                tokenType = AuthenticationOptions.TokenType,
                accessToken = token
            }
            );
        }


       


        private string GenerateToken(ApplicationUser user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>();
            foreach (var item in user.Claims)
            {
                claims.Add(new Claim(item.ClaimType, item.ClaimValue));
            }
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "TokenAuth"),
                 new[] {
                    new Claim("ID", user.Id.ToString()),
                    new Claim("role", "User" )
                }
            //claims.ToArray()
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
