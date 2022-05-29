using Sgi.Application.Dtos;
using Sgi.CrossCutting.Constants;

namespace Sgi.Application.Services.Validations
{
    public static class CompraValidation
    {
        public static string ValidarCompra(CompraDto compraDto)
        {
            if (compraDto.Total < 0)
                return "Valor da compra não pode ser negativo";

            if (string.IsNullOrWhiteSpace(compraDto.Data) || DateTime.TryParse(compraDto.Data, out _))
                return "Data da compra inválida";

            if (string.IsNullOrWhiteSpace(compraDto.FormaPagamento) ||
                (compraDto.FormaPagamento != FormaPagamentoConst.CarteiraVirtual && compraDto.FormaPagamento != FormaPagamentoConst.TransacaoBancaria))
                return "Forma de pagamento inválida";

            if (string.IsNullOrWhiteSpace(compraDto.UsuarioId))
                return "Cliente não informado";

            if (!compraDto.Ingressos.Any())
                return "Ingressos não informados";

            return null;
        }
    }
}
