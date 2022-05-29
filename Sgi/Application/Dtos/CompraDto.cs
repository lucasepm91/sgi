namespace Sgi.Application.Dtos
{
    public class CompraDto
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string Data { get; set; }
        public string FormaPagamento { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<IngressoDto> Ingressos { get; set; }
    }
}
