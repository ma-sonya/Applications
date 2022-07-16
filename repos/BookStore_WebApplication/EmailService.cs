using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace BookStore_WebApplication
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("BookShelf Administration", "log920021@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("log920021@gmail.com", "loginoff920022");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }        
    }
}
