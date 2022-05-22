namespace Sgi.Domain
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Email { get; set; }
        public decimal SaldoCarteira { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeResponsavel { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
        public Endereco Endereco { get; set; }
        public DadosBancarios DadosBancarios { get; set; }
    }
}
