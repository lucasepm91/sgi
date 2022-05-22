using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sgi.Application.Dtos;
using Sgi.CrossCutting.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sgi.Security
{
    public class GerarTokenService : IGerarTokenService
    {
        private readonly JwtOptions _jwtOptions;
        public GerarTokenService(IOptionsMonitor<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.CurrentValue;
        }

        public string GerarJwt(UsuarioDto usuarioDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions?.Key);
            var dataExpiracao = DateTime.Now.AddHours(4);

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuarioDto.Email),
                new Claim(ClaimTypes.Role, "usuario"),
                new Claim("Data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = dataExpiracao,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Audience = _jwtOptions?.Issuer,
                Issuer = _jwtOptions?.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
