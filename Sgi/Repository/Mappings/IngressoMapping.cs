using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgi.Domain;

namespace Sgi.Repository.Mappings
{
    public class IngressoMapping : IEntityTypeConfiguration<Ingresso>
    {
        public void Configure(EntityTypeBuilder<Ingresso> builder)
        {
            if (builder != null)
            {
                builder.ToTable("INGRESSO");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                builder.Property(i => i.EventoId).HasColumnName("EVENTO_ID").IsRequired();
                builder.Property(i => i.SessaoId).HasColumnName("SESSAO_ID").IsRequired();
                builder.Property(i => i.CompraId).HasColumnName("COMPRA_ID").IsRequired();
                builder.Property(i => i.Codigo).HasColumnName("CODIGO");
                builder.Property(i => i.Preco).HasColumnName("PRECO");

                builder.HasOne(i => i.Evento).WithOne().HasForeignKey<Ingresso>(i => i.EventoId);
                builder.HasOne(i => i.Sessao).WithOne().HasForeignKey<Ingresso>(i => i.SessaoId);

                builder.HasOne(i => i.Compra).WithMany(i => i.Ingressos).HasForeignKey(i => i.CompraId);
            }
        }
    }
}
