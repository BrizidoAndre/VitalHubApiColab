namespace WebAPI.Utils.Mail
{
    public class MailRequest
    {
        //destinatário
        public string? ToEmail { get; set; }

        //Assunto
        public string? Subject { get; set; }

        //Corpo do email
        public string? Body { get; set; }
    }
}
