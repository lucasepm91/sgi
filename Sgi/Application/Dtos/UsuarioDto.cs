namespace Sgi.Application.Dtos
{
    public class UsuarioDto
    {
		public string Id { get; set; }
		public string Nome { get; set; }
		public string CpfCnpj { get; set; }
		public string Email { get; set; }
		public string TipoUsuario { get; set; }
		public decimal SaldoCarteira { get; set; }
		public string Password { get; set; }
		public string NomeFantasia { get; set; }
		public string NomeResponsavel { get; set; }
		public string Pais { get; set; }
		public string Estado { get; set; }
		public string Cidade { get; set; }
		public string Bairro { get; set; }
		public string EnderecoCompleto { get; set; }
		public string Complemento { get; set; }		
	}
}
