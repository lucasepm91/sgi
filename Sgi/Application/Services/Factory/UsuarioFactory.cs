using Sgi.Application.Dtos;
using Sgi.Domain;

namespace Sgi.Application.Services.Factory
{
    public static class UsuarioFactory
    {
        public static UsuarioDto CriarUsuarioDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id.ToString(),
                Email = usuario.Email,
                CpfCnpj = usuario.CpfCnpj,
                Nome = usuario.Nome,
                SaldoCarteira = usuario.SaldoCarteira,
                Pais = usuario.Endereco?.Pais,
                Estado = usuario.Endereco?.Estado,
                Cidade = usuario.Endereco?.Cidade,
                Bairro = usuario.Endereco?.Bairro,
                EnderecoCompleto = usuario.Endereco?.EnderecoCompleto,
                Complemento = usuario.Endereco?.Complemento,
                TipoUsuario = usuario.TipoUsuario.ToString(),
                NomeFantasia = usuario.NomeFantasia,
                NomeResponsavel = usuario.NomeResponsavel
            };
        }

        public static Usuario CriarUsuario(UsuarioDto usuarioDto, string senha)
        {
            return new Usuario
            {
                Id = string.IsNullOrWhiteSpace(usuarioDto.Id) ? Guid.NewGuid() : new Guid(usuarioDto.Id),
                Email = usuarioDto.Email,
                CpfCnpj = usuarioDto.CpfCnpj,
                Nome = usuarioDto.Nome,
                Senha = !string.IsNullOrWhiteSpace(senha) ? senha : null,
                SaldoCarteira = usuarioDto.SaldoCarteira,
                TipoUsuario = usuarioDto.TipoUsuario.ToString(),
                NomeFantasia = usuarioDto.NomeFantasia,
                NomeResponsavel = usuarioDto.NomeResponsavel,
                Endereco = new Endereco
                {
                    Pais = usuarioDto.Pais,
                    Estado = usuarioDto.Estado,
                    Cidade = usuarioDto.Cidade,
                    Bairro = usuarioDto.Bairro,
                    EnderecoCompleto = usuarioDto.EnderecoCompleto,
                    Complemento = usuarioDto.Complemento
                },
                DadosBancarios = new DadosBancarios
                {
                    CodigoAgencia = string.Empty,
                    CodigoBanco = string.Empty,
                    CodigoConta = string.Empty
                }
            };
        }
    }
}
