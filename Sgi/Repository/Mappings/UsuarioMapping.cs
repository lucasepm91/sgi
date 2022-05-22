using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgi.Domain;

namespace Sgi.Repository.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            if (builder != null)
            {
                builder.ToTable("USUARIO");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                builder.Property(u => u.Nome).HasColumnName("NOME");
                builder.Property(u => u.CpfCnpj).HasColumnName("CPF_CNPJ");
                builder.Property(u => u.Email).HasColumnName("EMAIL");
                builder.Property(u => u.Senha).HasColumnName("SENHA");
                builder.Property(u => u.SaldoCarteira).HasColumnName("SALDO_CARTEIRA");
                builder.Property(u => u.NomeFantasia).HasColumnName("NOME_FANTASIA");
                builder.Property(u => u.NomeResponsavel).HasColumnName("NOME_RESPONSAVEL");
                builder.Property(u => u.Senha).HasColumnName("SENHA");
                builder.Property(u => u.TipoUsuario).HasColumnName("TIPO_USUARIO");

                builder.OwnsOne(u => u.Endereco, endereco =>
                {
                    endereco.Property(end => end.Pais).HasColumnName("PAIS");
                    endereco.Property(end => end.Estado).HasColumnName("ESTADO");
                    endereco.Property(end => end.Cidade).HasColumnName("CIDADE");
                    endereco.Property(end => end.Bairro).HasColumnName("BAIRRO");
                    endereco.Property(end => end.EnderecoCompleto).HasColumnName("ENDERECO_COMPLETO");
                    endereco.Property(end => end.Complemento).HasColumnName("COMPLEMENTO");
                });

                builder.OwnsOne(u => u.DadosBancarios, dadosBancarios =>
                {
                    dadosBancarios.Property(d => d.CodigoBanco).HasColumnName("CODIGO_BANCO");
                    dadosBancarios.Property(d => d.CodigoAgencia).HasColumnName("CODIGO_AGENCIA");
                    dadosBancarios.Property(d => d.CodigoConta).HasColumnName("CODIGO_CONTA");
                });                 
            }
        }
    }
}
