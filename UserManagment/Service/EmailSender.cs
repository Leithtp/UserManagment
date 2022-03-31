using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace UserManagment.Service
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string email, string body, string subject)
        {
            SmtpClient Client = new SmtpClient();
            Client.Host = _configuration.GetSection("AppSettings")["EmailSenderHost"];
            Client.Port = int.Parse(_configuration.GetSection("AppSettings")["EmailSenderPort"]);

            Client.Credentials = new NetworkCredential(_configuration.GetSection("AppSettings")["EmailSenderUserName"], _configuration.GetSection("AppSettings")["EmailSenderPassword"]);
            Client.EnableSsl = bool.Parse(_configuration.GetSection("AppSettings")["EmailSenderEnableSsl"]);


            MailMessage Message = new MailMessage(new MailAddress(_configuration.GetSection("AppSettings")["EmailSenderUserName"], "Администрация"), new MailAddress(email));

            Message.Subject = subject;
            Message.Body = body;
            

            //_userManager.token

            Message.IsBodyHtml = true;

            Client.Send(Message);

        }
    }

    public interface IEmailSender
    {
       void Send(string email, string body, string subject);
    }
}
