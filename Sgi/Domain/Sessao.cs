namespace Sgi.Domain
{
    public class Sessao
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public DateTime Data { get; set; }
        public int Lotacao { get; set; }
        public int IngressosVendidos { get; set; }
        public string CodigoLocal { get; set; }
        public MapaDeLugares MapaDeLugares { get; set; }
        public Evento Evento { get; set; }
    }
}
