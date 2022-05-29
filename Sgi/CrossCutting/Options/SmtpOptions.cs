namespace Sgi.CrossCutting.Options
{
    public class SmtpOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Remetente { get; set; }
        public string Password { get; set; }
        public bool Autenticacao { get; set; }
    }
}
