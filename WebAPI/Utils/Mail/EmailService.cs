using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace WebAPI.Utils.Mail
{
    public class EmailService : IEmailService
    {
        //variável privada com as ConfigurationSection do email
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            //obtem as configs do email e armazena na variável privada
            emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                //objeto que representa o email
                var email = new MimeMessage();

                //define o remetente do email
                email.Sender = MailboxAddress.Parse(emailSettings.Email);

                //adiciona o destinatário do email
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

                email.Subject = mailRequest.Subject;

                //cria o corpo do emial
                var builder = new BodyBuilder();

                //define o corpo do email como html
                builder.HtmlBody = mailRequest.Body;
                
                //define o corpo do email no obj MimeMessage
                email.Body = builder.ToMessageBody();

                //cria um client SMTP para envio de email
                using (var smtp = new SmtpClient())
                {
                    //Conecta-se ao servidor SMTP usando os dados do emailSettings
                    smtp.Connect
                        (emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);

                    //autentica-se no servidor SMTP usando os dados de Email Settings
                    smtp.Authenticate(emailSettings.Email, emailSettings.Password);

                    //envia o e-mail assíncrono
                    await smtp.SendAsync(email);
                }

            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
