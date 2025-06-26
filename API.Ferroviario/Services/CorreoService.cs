using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace API.Ferroviario.Services
{
    public class CorreoService
    {
        private readonly IConfiguration _config;

        public CorreoService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(string destino, string asunto, string mensaje)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("harolzambrano2005@gmail.com"));
            email.To.Add(MailboxAddress.Parse(destino));
            email.Subject = asunto;
            email.Body = new TextPart("plain") { Text = mensaje };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, false);
            await smtp.AuthenticateAsync("harolzambrano2005@gmail.com", "ettl pnay fjfy sqxs");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

}
