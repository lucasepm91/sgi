using Microsoft.Extensions.Options;
using Sgi.CrossCutting.Options;
using System.Net;
using System.Net.Mail;

namespace Sgi.DistributedServices
{
    public class EnvioEmailService : IEnvioEmailService
    {
        private readonly SmtpOptions _smtpOptions;

        public EnvioEmailService(IOptionsMonitor<SmtpOptions> options)
        {
            _smtpOptions = options.CurrentValue;
        }

        public SmtpClient CriarSmtpClient()
        {
            var client = new SmtpClient(_smtpOptions.Host)
            {
                Port = _smtpOptions.Port,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (_smtpOptions.Autenticacao)
            {
                client.Credentials = new NetworkCredential(_smtpOptions.Remetente, _smtpOptions.Password);
                client.EnableSsl = true;
            }

            return client;
        }


        public MailMessage CriarMensagemEmail(string assunto, string conteudo, string emailDestino)
        {
            var mensagem = new MailMessage
            {
                From = new MailAddress(_smtpOptions.Remetente),
                Subject = assunto,
                Body = conteudo,
                IsBodyHtml = false
            };

            mensagem.To.Add(emailDestino);            

            return mensagem;
        }

        public void EnviarEmail(SmtpClient smtpClient, MailMessage mailMessage) =>
            smtpClient.Send(mailMessage);
        
    }
}
