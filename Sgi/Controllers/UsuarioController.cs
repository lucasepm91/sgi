using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.CrossCutting.ApiConcerns;
using Sgi.CrossCutting.Exceptions;
using Sgi.Security;
using Swashbuckle.AspNetCore.Annotations;

namespace Sgi.Controllers
{
    [ApiController]
    [Route("sgi")]
    public class UsuarioController : ControllerBase
    {
        private readonly IGerarTokenService _gerarTokenService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IGerarTokenService gerarTokenService, IUsuarioService usuarioService)
        {
            _gerarTokenService = gerarTokenService;
            _usuarioService = usuarioService;
        }
        
        [HttpPost("login")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(TokenDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Autenticação para utilizar os recursos", "Verificação de identidade para geração de JWT")]
        public ActionResult<TokenDto> RealizarLogin([FromBody] LoginDto loginDto)
        {
            var usuario = _usuarioService.BuscarUsuarioParaAutenticacao(loginDto);
            if (usuario == null)
                throw new NaoAutorizadoException("Login ou senha inválidos");

            return new TokenDto
            {
                Type = "Bearer",
                Token = _gerarTokenService.GerarJwt(usuario)
            };
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("usuario/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(UsuarioDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar usuário por id", "Buscar usuário por id")]
        public ActionResult<UsuarioDto> BuscarUsuarioPorId([FromRoute] string id)
        {
            var usuario = _usuarioService.BuscarUsuarioPorId(id);
            if (usuario == null)
                throw new NaoEncontradoException("Usuário não encontrado!");

            return usuario;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("usuario/email/{email}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(UsuarioDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar usuário por email", "Buscar usuário por email")]
        public ActionResult<UsuarioDto> BuscarUsuarioPorEmail([FromRoute] string email)
        {
            var usuario = _usuarioService.BuscarUsuarioPorEmail(email);
            if (usuario == null)
                throw new NaoEncontradoException("Usuário não encontrado!");

            return usuario;
        }

        [HttpPost("usuario")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(UsuarioDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Cadastro de usuário", "Cadastro de usuário")]
        public async Task<ActionResult<UsuarioDto>> CadastrarUsuarioAsync([FromBody] UsuarioDto usuarioDto)
        {
            return await _usuarioService.CriarUsuarioAsync(usuarioDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("usuario/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(UsuarioDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Atualizar usuário", "Atualizar usuário")]
        public async Task<ActionResult<UsuarioDto>> AtualizarUsuarioAsync([FromBody] UsuarioDto usuarioDto)
        {
            return await _usuarioService.AtualizarUsuarioAsync(usuarioDto);  
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("usuario/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Deletar usuário", "Deletar usuário")]
        public async Task DeletarUsuario([FromRoute] string id)
        {
            await _usuarioService.DeletarUsuarioAsync(id);            
        }
    }
}
