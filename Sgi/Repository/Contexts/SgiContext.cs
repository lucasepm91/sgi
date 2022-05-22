using Microsoft.EntityFrameworkCore;
using Sgi.Domain;
using Sgi.Repository.Mappings;

namespace Sgi.Repository.Contexts
{
    public class SgiContext : DbContext
    {
        public SgiContext(DbContextOptions<SgiContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Ingresso> Ingresso { get; set; }
        public DbSet<Sessao> Sessao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.HasDefaultSchema("sgi");

                modelBuilder.ApplyConfiguration(new UsuarioMapping());
                modelBuilder.ApplyConfiguration(new EventoMapping());
                modelBuilder.ApplyConfiguration(new CompraMapping());
                modelBuilder.ApplyConfiguration(new IngressoMapping());
                modelBuilder.ApplyConfiguration(new SessaoMapping());
            }
        }
    }
}
