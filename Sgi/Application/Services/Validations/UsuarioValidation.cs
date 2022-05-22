using Sgi.Application.Dtos;
using Sgi.CrossCutting.Constants;

namespace Sgi.Application.Services.Validations
{
    public static class UsuarioValidation
    {
        public static string ValidarUsuario(UsuarioDto usuarioDto)
        {
			if (usuarioDto.Nome == null || usuarioDto.Nome.Length < 2 || usuarioDto.Nome.Length > 120)
				return "Nome do usuário não pode ser nulo e deve ter entre 2 e 120 caracteres";

			if (usuarioDto.CpfCnpj == null || (usuarioDto.CpfCnpj.Length != 11 && usuarioDto.CpfCnpj.Length != 14))
				return "Cpf ou Cnpj não pode ser nulo e deve ter 11 ou 14 caracteres";

			if (usuarioDto.Email == null || usuarioDto.Email.Length < 3 || usuarioDto.Email.Length > 100 || !usuarioDto.Email.Contains('@'))
				return "Email não pode ser nulo, deve ser válido e deve ter entre 3 e 100 caracteres";

			if (usuarioDto.Pais == null || usuarioDto.Pais.Length < 2 || usuarioDto.Pais.Length > 100)
				return "Pais não pode ser nulo e deve ter entre 2 e 100 caracteres";

			if (usuarioDto.Estado == null || usuarioDto.Estado.Length < 2 || usuarioDto.Estado.Length > 100)
				return "Estado não pode ser nulo e deve ter entre 2 e 100 caracteres";

			if (usuarioDto.Cidade == null || usuarioDto.Cidade.Length < 2 || usuarioDto.Cidade.Length > 100)
				return "Cidade não pode ser nulo e deve ter entre 2 e 100 caracteres";

			if (usuarioDto.Bairro == null || usuarioDto.Bairro.Length < 2 || usuarioDto.Bairro.Length > 100)
				return "Bairro não pode ser nulo e deve ter entre 2 e 100 caracteres";

			if (usuarioDto.EnderecoCompleto == null || usuarioDto.EnderecoCompleto.Length < 2 || usuarioDto.EnderecoCompleto.Length > 200)
				return "Endereço não pode ser nulo e deve ter entre 2 e 200 caracteres";

			if (usuarioDto.Complemento == null || usuarioDto.Complemento.Length < 2 || usuarioDto.Complemento.Length > 150)
				return "Complemento não pode ser nulo e deve ter entre 2 e 150 caracteres";

			if (usuarioDto.TipoUsuario == null || (usuarioDto.TipoUsuario != TipoUsuarioConst.Cliente && usuarioDto.TipoUsuario != TipoUsuarioConst.Organizador))
				return "Tipo de usuário não pode ser nulo e deve ser cliente ou organizador";

			if (decimal.Compare(usuarioDto.SaldoCarteira, 0) < 0)
				return "Saldo carteira não pode ser negativo";

			if (usuarioDto.Password == null || usuarioDto.Password.Length < 4 || usuarioDto.Password.Length > 10)
				return "Senha não pode ser nula e deve ter entre 4 e 10 caracteres";

			return null;
		}
    }
}
