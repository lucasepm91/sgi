using System.Net.Mail;

namespace Sgi.DistributedServices
{
    public interface IEnvioEmailService
    {
        SmtpClient CriarSmtpClient();
        MailMessage CriarMensagemEmail(string assunto, string conteudo, string emailDestino);
        void EnviarEmail(SmtpClient smtpClient, MailMessage mailMessage);
    }
}
