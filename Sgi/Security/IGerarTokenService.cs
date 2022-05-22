using Sgi.Application.Dtos;

namespace Sgi.Security
{
    public interface IGerarTokenService
    {
        string GerarJwt(UsuarioDto usuarioDto);
    }
}
