namespace Sgi.Application.Dtos
{
    public class IngressoDto
    {
        public string Id { get; set; }
        public string EventoId { get; set; }
        public string SessaoId { get; set; }
        public string CompraId { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
        public string NomeEvento { get; set; }
        public string DataSessao { get; set; }
        public string Endereco { get; set; }
        public string LinkStream { get; set; }
    }
}
