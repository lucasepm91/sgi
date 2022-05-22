using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Sgi.CrossCutting.Options;
using System.Text;

namespace Sgi.Security
{
    public class CriptografiaService : ICriptografiaService
    {
        private readonly SecurityOptions _securityOptions;
        public CriptografiaService(IOptionsMonitor<SecurityOptions> options)
        {
            _securityOptions = options.CurrentValue;
        }

        public string Criptografar(string senha)
        {            
            string chave = _securityOptions.Key;
            byte[] salt = Encoding.UTF8.GetBytes(chave);
            
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
