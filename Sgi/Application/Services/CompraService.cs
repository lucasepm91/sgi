using Sgi.Application.Dtos;
using Sgi.Application.Interfaces;
using Sgi.Application.Services.Factory;
using Sgi.Application.Services.Validations;
using Sgi.CrossCutting.Constants;
using Sgi.CrossCutting.Exceptions;
using Sgi.Domain;
using Sgi.Repository;

namespace Sgi.Application.Services
{
    public class CompraService : ICompraService
    {
        private readonly ISgiRepository _sgiRepository;

        public CompraService(ISgiRepository sgiRepository)
        {
            _sgiRepository = sgiRepository;
        }

        public IEnumerable<CompraDto> BuscarHistoricoCompras(string id)
        {
            var compras = _sgiRepository.BuscarHistoricoCompras(new Guid(id));

            if (compras == null || !compras.Any())
                throw new NaoEncontradoException("Compras não encontradas!");

            var historico = new List<CompraDto>();

            foreach(var compra in compras)
                historico.Add(CompraFactory.CriarCompraDto(compra));

            return historico;
        }

        public async Task<CompraDto> ProcessarCompraAsync(CompraDto compraDto)
        {
            string validacao = CompraValidation.ValidarCompra(compraDto);

            if (validacao != null)
                throw new RegraDeNegocioException(validacao);

            var ingressos = new List<Ingresso>();

            foreach (var ingressoDto in compraDto.Ingressos)
            {
                var sessao = _sgiRepository.BuscarSessaoPorId(new Guid(ingressoDto.SessaoId));
                var evento = _sgiRepository.BuscarEventoPorId(new Guid(ingressoDto.EventoId));
                ingressos.Add(CompraFactory.CriarIngresso(ingressoDto, sessao, evento));
                
                sessao.IngressosVendidos++;
                if(evento.Modalidade == ModalidadeConst.Presencial)
                    AtualizarLugaresAposCompra(sessao, ingressoDto);
            }

            var compra = CompraFactory.CriarCompra(compraDto, ingressos);

            try
            {
                await _sgiRepository.InserirCompraAsync(compra);
                await _sgiRepository.CommitAsync();

                foreach (var ingresso in ingressos)
                    ingresso.CompraId = compra.Id;

                await _sgiRepository.InserirIngressosAsync(ingressos);
                await _sgiRepository.CommitAsync();
            }
            catch(Exception ex)
            {
                throw new ErroInternoException(ex.Message);
            }            

            return compraDto;
        }

        private void AtualizarLugaresAposCompra(Sessao sessao, IngressoDto ingressoDto)
        {
            var descricaoReservados = string.IsNullOrWhiteSpace(sessao.MapaDeLugares.Reservados) ? ingressoDto.Codigo.ToUpperInvariant()
                                                                                : sessao.MapaDeLugares.Reservados + "|" + ingressoDto.Codigo.ToUpperInvariant();
            sessao.MapaDeLugares.Reservados = descricaoReservados;
            var descricaoLivres = sessao.MapaDeLugares.Livres.Split('|').ToList();
            var filtrados = descricaoLivres.Where(l => l.Contains(ingressoDto.Codigo));
            var descricaoFiltrada = string.Empty;

            foreach (var item in filtrados)
            {
                if (descricaoFiltrada == string.Empty)
                    descricaoFiltrada += item;
                else
                    descricaoFiltrada += "|" + item;
            }
            sessao.MapaDeLugares.Livres = descricaoFiltrada;
        }
    }
}
