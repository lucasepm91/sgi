using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgi.Domain;

namespace Sgi.Repository.Mappings
{
    public class EventoMapping : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            if (builder != null)
            {
                builder.ToTable("EVENTO");
                builder.HasKey(ev => ev.Id);
                builder.Property(ev => ev.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                builder.Property(ev => ev.UsuarioId).HasColumnName("USUARIO_ID").IsRequired();
                builder.Property(ev => ev.Nome).HasColumnName("NOME");
                builder.Property(ev => ev.Tipo).HasColumnName("TIPO_EVENTO");
                builder.Property(ev => ev.Modalidade).HasColumnName("MODALIDADE");
                builder.Property(ev => ev.Descricao).HasColumnName("DESCRICAO");                
                builder.Property(ev => ev.LinkStream).HasColumnName("LINK_STREAM");
                builder.Property(ev => ev.LinkRedirecionamento).HasColumnName("LINK_REDIRECIONAMENTO");
                builder.Property(ev => ev.Preco).HasColumnName("PRECO");

                builder.OwnsOne(ev => ev.Endereco, endereco =>
                {
                    endereco.Property(end => end.Pais).HasColumnName("PAIS");
                    endereco.Property(end => end.Estado).HasColumnName("ESTADO");
                    endereco.Property(end => end.Cidade).HasColumnName("CIDADE");
                    endereco.Property(end => end.Bairro).HasColumnName("BAIRRO");
                    endereco.Property(end => end.EnderecoCompleto).HasColumnName("ENDERECO_COMPLETO");
                    endereco.Property(end => end.Complemento).HasColumnName("COMPLEMENTO");
                });

                builder.HasOne(ev => ev.Organizador).WithOne().HasForeignKey<Evento>(ev => ev.UsuarioId);

                builder.Navigation(builder => builder.Endereco).IsRequired();
                builder.Navigation(builder => builder.Organizador).IsRequired();
                builder.Navigation(builder => builder.Sessoes);
            }
        }
    }
}
