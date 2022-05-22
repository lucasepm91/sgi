using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgi.Domain;

namespace Sgi.Repository.Mappings
{
    public class SessaoMapping : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            if (builder != null)
            {
                builder.ToTable("SESSAO");
                builder.HasKey(s => s.Id);
                builder.Property(s => s.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                builder.Property(s => s.EventoId).HasColumnName("EVENTO_ID").IsRequired();
                builder.Property(s => s.Data).HasColumnName("DATA");
                builder.Property(s => s.Lotacao).HasColumnName("LOTACAO");
                builder.Property(s => s.IngressosVendidos).HasColumnName("INGRESSOS_VENDIDOS");
                builder.Property(s => s.CodigoLocal).HasColumnName("CODIGO_LOCAL");

                builder.OwnsOne(s => s.MapaDeLugares, mapaDeLugares =>
                {
                    mapaDeLugares.Property(m => m.Esquema).HasColumnName("ESQUEMA");
                    mapaDeLugares.Property(m => m.Reservados).HasColumnName("RESERVADOS");
                    mapaDeLugares.Property(m => m.Livres).HasColumnName("LIVRES");
                });

                builder.HasOne(s => s.Evento).WithMany(ev => ev.Sessoes).HasForeignKey(s => s.EventoId);
            }
        }
    }
}
