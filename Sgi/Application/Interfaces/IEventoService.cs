using Sgi.Application.Dtos;

namespace Sgi.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> CriarEventoAsync(EventoDto eventoDto);
        EventoDto BuscarEventoPorId(string id);
    }
}
