namespace Sgi.Application.Dtos
{
    public class EventoDto
    {
        public string Id { get; set; }
        public string OrganizadorId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Modalidade { get; set; }
        public string Descricao { get; set; }
        public string LinkStream { get; set; }
        public string LinkRedirecionamento { get; set; }
        public decimal Preco { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Complemento { get; set; }
        public UsuarioDto Organizador { get; set; }
        public List<SessaoDto> Sessoes { get; set; }
    }
}
