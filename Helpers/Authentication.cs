using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Digital.Helpers
{
    public class Authentication
    {

        public static RSAParameters GenerateRSAKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}
