using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.Application.Services.Factory;
using Sgi.Application.Services.Validations;
using Sgi.CrossCutting.Exceptions;
using Sgi.Domain;
using Sgi.Repository;
using Sgi.Security;

namespace Sgi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ICriptografiaService _criptografiaService;
        private readonly ISgiRepository _sgiRepository;

        public UsuarioService(ICriptografiaService criptografiaService, ISgiRepository sgiRepository)
        {
            _criptografiaService = criptografiaService;
            _sgiRepository = sgiRepository;
        }

        public UsuarioDto BuscarUsuarioParaAutenticacao(LoginDto loginDto)
        {
            Usuario usuario = _sgiRepository.BuscarUsuarioPorEmail(loginDto.Email);
            string senhaCriptografada = _criptografiaService.Criptografar(loginDto.Password);

            if (usuario != null && (usuario.Senha == senhaCriptografada))
                return UsuarioFactory.CriarUsuarioDto(usuario);

            return null;
        }

        public UsuarioDto BuscarUsuarioPorEmail(string email)
        {
            Usuario usuario = _sgiRepository.BuscarUsuarioPorEmail(email);
            if (usuario != null)
                return UsuarioFactory.CriarUsuarioDto(usuario);

            return null;
        }

        public UsuarioDto BuscarUsuarioPorId(string id)
        {
            Usuario usuario = _sgiRepository.BuscarUsuarioPorId(new Guid(id));
            if (usuario != null)
                return UsuarioFactory.CriarUsuarioDto(usuario);

            return null;
        }

        public async Task<UsuarioDto> CriarUsuarioAsync(UsuarioDto usuarioDto)
        {
            string validacao = UsuarioValidation.ValidarUsuario(usuarioDto);

            if (validacao != null)
                throw new RegraDeNegocioException(validacao);

            string senha = _criptografiaService.Criptografar(usuarioDto.Password);
            Usuario usuario = UsuarioFactory.CriarUsuario(usuarioDto, senha);

            await _sgiRepository.InserirUsuarioAsync(usuario);
            await _sgiRepository.CommitAsync();
            usuarioDto.Password = string.Empty;

            return usuarioDto; 
        }

        public async Task DeletarUsuarioAsync(string id)
        {
            Usuario usuario = _sgiRepository.BuscarUsuarioPorId(new Guid(id));

            if (usuario != null)
            {
                _sgiRepository.DeletarUsuario(usuario);
                await _sgiRepository.CommitAsync();
            }
            else
                throw new NaoEncontradoException("Usuário não encontrado!");
        }

        public async Task<UsuarioDto> AtualizarUsuarioAsync(UsuarioDto usuarioDto)
        {
            string validacao = UsuarioValidation.ValidarUsuario(usuarioDto);
            Usuario encontrado = _sgiRepository.BuscarUsuarioPorId(new Guid(usuarioDto.Id));

            if (validacao != null)
                throw new RegraDeNegocioException(validacao);

            if (encontrado == null)
                throw new NaoEncontradoException("Usuário não encontrado!");
            
            encontrado.Nome = usuarioDto.Nome;
            encontrado.NomeFantasia = usuarioDto.NomeFantasia;
            encontrado.NomeResponsavel = usuarioDto.NomeResponsavel;
            encontrado.CpfCnpj = usuarioDto.CpfCnpj;
            encontrado.Endereco.Pais = usuarioDto.Pais;
            encontrado.Endereco.Estado = usuarioDto.Estado;
            encontrado.Endereco.Cidade = usuarioDto.Cidade;
            encontrado.Endereco.Bairro = usuarioDto.Bairro;
            encontrado.Endereco.EnderecoCompleto = usuarioDto.EnderecoCompleto;
            encontrado.Endereco.Complemento = usuarioDto.Complemento;
            encontrado.TipoUsuario = usuarioDto.TipoUsuario;

            await _sgiRepository.CommitAsync();
            return UsuarioFactory.CriarUsuarioDto(encontrado);
        }

        public async Task<UsuarioDto> AdicionarValorCarteiraAsync(string id, string codigo)
        {
            Usuario usuario = _sgiRepository.BuscarUsuarioPorId(new Guid(id));
            codigo = codigo ?? string.Empty;
            int.TryParse(codigo.Substring(3, 5), out int valor);

            if (usuario == null)
                throw new NaoEncontradoException("Usuário não encontrado!");

            if (codigo.Length != 5 || (valor != 20 && valor != 50))
            {
                throw new RegraDeNegocioException("Código inválido");
            }

            usuario.SaldoCarteira += valor;
            await _sgiRepository.CommitAsync();
            return UsuarioFactory.CriarUsuarioDto(usuario);
        }
    }
}
