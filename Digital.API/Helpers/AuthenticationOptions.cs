using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.API.Helpers
{
    public class AuthenticationOptions
    {
        public static string Audience { get; } = "TokenAudience";
        public static string Issuer { get; } = "TokenIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(Authentication.GenerateRSAKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";
    }
}
