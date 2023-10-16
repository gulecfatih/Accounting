using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace AccountingWebApi.Data.Logic
{
    public class EmailServiceLogic
    {
        private readonly SmtpClient _smtpClient;
        public EmailServiceLogic()
        {
            _smtpClient = new SmtpClient();
        }
        public void SendEmail(string recipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Deneme Deneme", "deneme@gmail.com"));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            _smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            _smtpClient.Authenticate("deneme@gmail.com", "Deneme*"); // denendi ve böyle bırakıldı
            _smtpClient.Send(message);
            _smtpClient.Disconnect(true);
        }
    }
}
