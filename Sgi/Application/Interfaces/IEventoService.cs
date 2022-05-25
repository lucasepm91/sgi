using Sgi.Application.Dtos;

namespace Sgi.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> CriarEventoAsync(EventoDto eventoDto);
        EventoDto BuscarEventoPorId(string id);
        EventoDto BuscarEventoPorNome(string nome);
        IEnumerable<EventoDto> BuscarEventoPorTipo(string tipo);
        IEnumerable<EventoDto> BuscarHistoricoEventos(string id);
    }
}
