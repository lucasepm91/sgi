using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.CrossCutting.ApiConcerns;
using Swashbuckle.AspNetCore.Annotations;

namespace Sgi.Controllers
{
    [ApiController]
    [Route("sgi")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("evento/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(EventoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar evento por id", "Buscar evento por id")]
        public EventoDto BuscarEventoPorIdAsync([FromRoute] string id)
        {
            return _eventoService.BuscarEventoPorId(id);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("evento/nome/{nome}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(EventoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar evento por nome", "Buscar evento por nome")]
        public EventoDto BuscarEventoPorNomeAsync([FromRoute] string nome)
        {
            return _eventoService.BuscarEventoPorNome(nome);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("evento/tipo/{tipo}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(IEnumerable<EventoDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar evento por tipo", "Buscar evento por tipo")]
        public IEnumerable<EventoDto> BuscarEventoPorTipoAsync([FromRoute] string tipo)
        {
            return _eventoService.BuscarEventoPorTipo(tipo);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("evento")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(EventoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Cadastro de evento", "Cadastro de evento")]
        public async Task<ActionResult<EventoDto>> CadastrarEventoAsync([FromBody] EventoDto eventoDto)
        {
            return await _eventoService.CriarEventoAsync(eventoDto);
        }
    }
}
