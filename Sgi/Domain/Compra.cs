namespace Sgi.Domain
{
    public class Compra
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public string FormaPagamento { get; set; }
        public decimal Total { get; set; }
        public Usuario Cliente { get; set; }
        public IEnumerable<Ingresso> Ingressos { get; set; }        
    }
}
