using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.Application.Services.Factory;
using Sgi.Application.Services.Validations;
using Sgi.CrossCutting.Exceptions;
using Sgi.Domain;
using Sgi.Repository;

namespace Sgi.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly ISgiRepository _sgiRepository;

        public EventoService(ISgiRepository sgiRepository)
        {
            _sgiRepository = sgiRepository;
        }

        public async Task<EventoDto> CriarEventoAsync(EventoDto eventoDto)
        {
            string validacao = EventoValidation.ValidarEvento(eventoDto);

            if (validacao != null)
                throw new RegraDeNegocioException(validacao);

            Usuario organizador = _sgiRepository.BuscarUsuarioPorId(new Guid(eventoDto.OrganizadorId));

            if (organizador == null)
                throw new NaoEncontradoException("Organizador não encontrado!");

            Evento evento = EventoFactory.CriarEvento(eventoDto, organizador);
            var sessoes = evento.Sessoes.Select(s => s);
            evento.Sessoes = null;
            try
            {
                await _sgiRepository.InserirEventoAsync(evento);
                await _sgiRepository.CommitAsync();

                foreach (var sessao in sessoes)
                {
                    sessao.EventoId = evento.Id;                    
                }
                
                await _sgiRepository.InserirSessoesAsync(sessoes);
                await _sgiRepository.CommitAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            evento.Sessoes = sessoes.ToList();

            return eventoDto;
        }
    }
}
