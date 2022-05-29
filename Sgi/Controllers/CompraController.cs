using Microsoft.AspNetCore.Mvc;
using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.CrossCutting.ApiConcerns;
using Swashbuckle.AspNetCore.Annotations;

namespace Sgi.Controllers
{
    [ApiController]
    [Route("sgi")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost("compra")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(CompraDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Processamento de compra", "Processamento de compra")]
        public async Task<ActionResult<CompraDto>> ProcessarCompraAsync([FromBody] CompraDto compraDto)
        {
            return await _compraService.ProcessarCompraAsync(compraDto);
        }

        [HttpGet("compra/historico/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Operação realizada com sucesso", typeof(IEnumerable<CompraDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha na operação", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Falha no processamento da requisição", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor", typeof(ResponseErro))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Falha na operação", typeof(ResponseErro))]
        [SwaggerOperation("Buscar histórico de compras", "Buscar histórico de compras")]
        public IEnumerable<CompraDto> BuscarHistoricoCompras([FromRoute] string id)
        {
            return _compraService.BuscarHistoricoCompras(id);
        }
    }
}
