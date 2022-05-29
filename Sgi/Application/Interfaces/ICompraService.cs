using Sgi.Application.Dtos;

namespace Sgi.Application.Interfaces
{
    public interface ICompraService
    {
        IEnumerable<CompraDto> BuscarHistoricoCompras(string id);
        Task<CompraDto> ProcessarCompraAsync(CompraDto compraDto);
    }
}
