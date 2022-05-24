using Sgi.Application.Dtos;
using Sgi.Domain;

namespace Sgi.Application.Services.Factory
{
    public static class EventoFactory
    {
        public static EventoDto CriarEventoDto(Evento evento)
        {
            var eventoDto = new EventoDto
            {
                Id = evento.Id.ToString(),
                OrganizadorId = evento.Organizador.Id.ToString(),
                Nome = evento.Nome,
                Tipo = evento.Tipo,
                Modalidade = evento.Modalidade,
                Descricao = evento.Descricao,
                LinkRedirecionamento = evento.LinkRedirecionamento,
                LinkStream = evento.LinkStream,
                Preco = evento.Preco,
                Pais = evento.Endereco.Pais,
                Estado = evento.Endereco.Estado,
                Cidade = evento.Endereco.Cidade,
                Bairro = evento.Endereco.Bairro,
                EnderecoCompleto = evento.Endereco.EnderecoCompleto,
                Complemento = evento.Endereco.Complemento,
                Organizador = UsuarioFactory.CriarUsuarioDto(evento.Organizador),
                Sessoes = new List<SessaoDto>()
            };

            if (evento.Sessoes != null && evento.Sessoes.Any())
            {
                foreach (var sessao in evento.Sessoes)
                {
                    var sessaoDto = new SessaoDto
                    {
                        Id = sessao.Id.ToString(),
                        EventoId = evento.Id.ToString(),
                        CodigoLocal = sessao.CodigoLocal,
                        Data = sessao.Data,
                        Lotacao = sessao.Lotacao,
                        IngressosVendidos = sessao.IngressosVendidos,
                        Esquema = sessao.MapaDeLugares.Esquema,
                        Reservados = sessao.MapaDeLugares.Reservados,
                        Livres = sessao.MapaDeLugares.Livres
                    };
                    eventoDto.Sessoes.Add(sessaoDto);
                }
            }

            return eventoDto;
        }

        public static Evento CriarEvento(EventoDto eventoDto, Usuario organizador)
        {
            var evento = new Evento
            {
                Id = string.IsNullOrWhiteSpace(eventoDto.Id) ? Guid.NewGuid() : new Guid(eventoDto.Id),
                UsuarioId = organizador.Id,
                Nome = eventoDto.Nome,
                Tipo = eventoDto.Tipo,
                Modalidade = eventoDto.Modalidade,
                Descricao = eventoDto.Descricao,
                LinkRedirecionamento = eventoDto.LinkRedirecionamento,
                LinkStream = eventoDto.LinkStream,
                Preco = eventoDto.Preco,
                Organizador = organizador,
                Endereco = new Endereco
                {
                    Pais = eventoDto.Pais,
                    Estado = eventoDto.Estado,
                    Cidade = eventoDto.Cidade,
                    Bairro = eventoDto.Bairro,
                    EnderecoCompleto = eventoDto.EnderecoCompleto,
                    Complemento = eventoDto.Complemento
                },                
                Sessoes = new List<Sessao>()
            };

            if (eventoDto.Sessoes != null && eventoDto.Sessoes.Any())
            {
                foreach (var sessaoDto in eventoDto.Sessoes)
                {
                    var sessao = new Sessao
                    {
                        Id = string.IsNullOrWhiteSpace(sessaoDto.Id) ? Guid.NewGuid() : new Guid(sessaoDto.Id),
                        EventoId = evento.Id,
                        CodigoLocal = sessaoDto.CodigoLocal,
                        Data = sessaoDto.Data,
                        Lotacao = sessaoDto.Lotacao,
                        IngressosVendidos = sessaoDto.IngressosVendidos,
                        MapaDeLugares = new MapaDeLugares
                        {
                            Esquema = sessaoDto.Esquema,
                            Reservados = sessaoDto.Reservados,
                            Livres = sessaoDto.Livres
                        }
                    };
                    evento.Sessoes.Add(sessao);
                }
            }

            return evento;
        }
    }
}
