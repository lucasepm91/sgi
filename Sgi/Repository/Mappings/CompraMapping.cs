using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgi.Domain;

namespace Sgi.Repository.Mappings
{
    public class CompraMapping : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            if (builder != null)
            {
                builder.ToTable("COMPRA");
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                builder.Property(c => c.UsuarioId).HasColumnName("USUARIO_ID").IsRequired();
                builder.Property(c => c.Data).HasColumnName("DATA");
                builder.Property(c => c.FormaPagamento).HasColumnName("FORMA_PAGAMENTO");
                builder.Property(c => c.Total).HasColumnName("TOTAL");

                builder.HasOne(c => c.Cliente).WithOne().HasForeignKey<Compra>(c => c.UsuarioId);

                builder.Navigation(builder => builder.Cliente).IsRequired();
                builder.Navigation(builder => builder.Ingressos);
            }
        }
    }
}
