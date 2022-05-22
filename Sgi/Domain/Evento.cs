namespace Sgi.Domain
{
    public class Evento
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Modalidade { get; set; }
        public string Descricao { get; set; }        
        public string LinkStream { get; set; }        
        public string LinkRedirecionamento { get; set; }
        public decimal Preco { get; set; }
        public Usuario Organizador { get; set; }
        public Endereco Endereco { get; set; }
        public List<Sessao> Sessoes { get; set; }
    }
}
