namespace Sgi.Application.Dtos
{
    public class SessaoDto
    {
        public string Id { get; set; }
        public string EventoId { get; set; }
        public DateTime Data { get; set; }
        public int Lotacao { get; set; }
        public int IngressosVendidos { get; set; }
        public string CodigoLocal { get; set; }
        public string Esquema { get; set; }
        public string Reservados { get; set; }
        public string Livres { get; set; }
    }
}
