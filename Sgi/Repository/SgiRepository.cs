using Microsoft.EntityFrameworkCore;
using Sgi.Domain;
using Sgi.Repository.Contexts;

namespace Sgi.Repository
{
    public class SgiRepository : ISgiRepository
    {        
        private SgiContext _context;

        public SgiRepository(SgiContext context)
        {
            _context = context;
        }        

        public async Task CommitAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);
        public Usuario BuscarUsuarioPorEmail(string email) => _context.Usuario.Where(u => u.Email == email).FirstOrDefault();
        public Usuario BuscarUsuarioPorId(Guid id) => _context.Usuario.Where(u => u.Id == id).FirstOrDefault();
        public async Task InserirUsuarioAsync(Usuario usuario) => await _context.Usuario.AddAsync(usuario).ConfigureAwait(false);
        public void DeletarUsuario(Usuario usuario) => _context.Usuario.Remove(usuario);

        public async Task InserirEventoAsync(Evento evento) => await _context.Evento.AddAsync(evento).ConfigureAwait(false);
        public async Task InserirSessoesAsync(IEnumerable<Sessao> sessoes) => await _context.Sessao.AddRangeAsync(sessoes).ConfigureAwait(false);
        public async Task InserirSessaoAsync(Sessao sessao) => await _context.Sessao.AddAsync(sessao).ConfigureAwait(false);
        public Evento BuscarEventoPorId(Guid id) => _context.Evento.Where(ev => ev.Id == id).Include(ev => ev.Sessoes).Include(ev => ev.Organizador).FirstOrDefault();
    }
}
