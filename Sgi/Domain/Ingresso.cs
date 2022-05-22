namespace Sgi.Domain
{
    public class Ingresso
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public Guid SessaoId { get; set; }
        public Guid CompraId { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
        public Evento Evento { get; set; }
        public Sessao Sessao { get; set; }
        public Compra Compra { get; set; }
    }
}
