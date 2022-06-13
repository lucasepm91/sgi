using Sgi.Application.Dtos;
using Sgi.CrossCutting.Constants;
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
            string endereco = null;

            if (!string.IsNullOrWhiteSpace(ingresso.Evento.Endereco?.EnderecoCompleto))
            {
                string complemento = !string.IsNullOrWhiteSpace(ingresso.Evento.Endereco.Complemento) ? ", " + ingresso.Evento.Endereco.Complemento : string.Empty;
                endereco = ingresso.Evento.Endereco.EnderecoCompleto + complemento;
            }
            return new IngressoDto
            {
                Id = ingresso.Id.ToString(),
                Codigo = ingresso.Codigo,
                Preco = ingresso.Preco,
                CompraId = ingresso.CompraId.ToString(),
                SessaoId = ingresso.SessaoId.ToString(),
                EventoId = ingresso.EventoId.ToString(),
                NomeEvento = ingresso.Evento.Nome,
                DataSessao = ingresso.Sessao.Data.ToString("dd/MM/yyyy HH:mm"),
                LinkStream = ingresso.Evento.LinkStream,
                Endereco =  endereco
            };
        }

        public static Ingresso CriarIngresso(IngressoDto ingressoDto, Sessao sessao, Evento evento)
        {
            return new Ingresso
            {
                Id = Guid.NewGuid(),
                Codigo = evento.Modalidade == ModalidadeConst.Presencial ? ingressoDto.Codigo : Guid.NewGuid().ToString(),
                Preco = ingressoDto.Preco,
                SessaoId = new Guid(ingressoDto.SessaoId),
                EventoId = new Guid(ingressoDto.EventoId),
                Sessao = sessao,
                Evento = evento
            };
        }
    }
}
