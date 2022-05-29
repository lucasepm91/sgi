using Sgi.Application.Dtos;
using Sgi.Domain;

namespace Sgi.Application.Services.Factory
{
    public static class CompraFactory
    {
        public static CompraDto CriarCompraDto(Compra compra)
        {
            var ingressos = new List<IngressoDto>();
            foreach(var ingresso in compra.Ingressos)
            {
                ingressos.Add(CriarIngressoDto(ingresso));
            }

            return new CompraDto
            {
                Id = compra.Id.ToString(),
                Data = compra.Data.ToString("dd/MM/yyyy HH:mm:ss"),
                FormaPagamento = compra.FormaPagamento,
                Total = compra.Total,
                UsuarioId = compra.UsuarioId.ToString(),
                Ingressos = ingressos
            };
        }

        public static Compra CriarCompra(CompraDto compraDto, IEnumerable<Ingresso> ingressos)
        {            
            var compra = new Compra
            {
                Id = Guid.NewGuid(),
                Data = DateTime.Now,
                FormaPagamento = compraDto.FormaPagamento,
                Total = compraDto.Total,
                UsuarioId = new Guid(compraDto.UsuarioId)
            };

            foreach (var ingresso in ingressos)
            {
                ingresso.CompraId = compra.Id;
                ingresso.Compra = compra;
            }

            compra.Ingressos = ingressos;

            return compra;
        }

        public static IngressoDto CriarIngressoDto(Ingresso ingresso)
        {
            return new IngressoDto
            {
                Id = ingresso.Id.ToString(),
                Codigo = ingresso.Codigo,
                Preco = ingresso.Preco,
                CompraId = ingresso.CompraId.ToString(),
                SessaoId = ingresso.SessaoId.ToString(),
                EventoId = ingresso.EventoId.ToString()
            };
        }

        public static Ingresso CriarIngresso(IngressoDto ingressoDto, Sessao sessao, Evento evento)
        {
            return new Ingresso
            {
                Id = Guid.NewGuid(),
                Codigo = ingressoDto.Codigo,
                Preco = ingressoDto.Preco,
                SessaoId = new Guid(ingressoDto.SessaoId),
                EventoId = new Guid(ingressoDto.EventoId),
                Sessao = sessao,
                Evento = evento
            };
        }
    }
}
