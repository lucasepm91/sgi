using Sgi.Application.Dtos;
using Sgi.Security;

namespace Sgi.Application.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDto BuscarUsuarioParaAutenticacao(LoginDto loginDto);
        UsuarioDto BuscarUsuarioPorId(string id);
        UsuarioDto BuscarUsuarioPorEmail(string email);
        Task<UsuarioDto> CriarUsuarioAsync(UsuarioDto usuarioDto);
        Task DeletarUsuarioAsync(string id);
        Task<UsuarioDto> AtualizarUsuarioAsync(UsuarioDto usuarioDto);
        Task<UsuarioDto> AdicionarValorCarteiraAsync(string id, string codigo);
    }
}
