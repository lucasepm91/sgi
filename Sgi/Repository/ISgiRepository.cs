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
        Sessao BuscarSessaoPorId(Guid id);
        Task InserirSessaoAsync(Sessao sessao);
        Task InserirSessoesAsync(IEnumerable<Sessao> sessoes);
        Evento BuscarEventoPorId(Guid id);
        Evento BuscarEventoPorNome(string nome);
        IEnumerable<Evento> BuscarEventoPorTipo(string tipo);
        IEnumerable<Evento> BuscarHistoricoEventos(Guid idOrganizador);

        IEnumerable<Compra> BuscarHistoricoCompras(Guid idCliente);
        Task InserirIngressosAsync(IEnumerable<Ingresso> ingressos);
        Task InserirCompraAsync(Compra compra);
    }
}
