using Sgi.Domain;

namespace Sgi.Repository
{
    public interface ISgiRepository
    {        
        Task CommitAsync();        
        Usuario BuscarUsuarioPorEmail(string email);
        Usuario BuscarUsuarioPorId(Guid id);
        Task InserirUsuarioAsync(Usuario usuario);
        void DeletarUsuario(Usuario usuario);

        Task InserirEventoAsync(Evento evento);
        Task InserirSessaoAsync(Sessao sessao);
        Task InserirSessoesAsync(IEnumerable<Sessao> sessoes);
        Evento BuscarEventoPorId(Guid id);
    }
}
