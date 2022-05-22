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
        [HttpPost("evento")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(EventoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Cadastro de evento", "Cadastro de evento")]
        public async Task<ActionResult<EventoDto>> CadastrarUsuarioAsync([FromBody] EventoDto eventoDto)
        {
            return await _eventoService.CriarEventoAsync(eventoDto);
        }
    }
}
