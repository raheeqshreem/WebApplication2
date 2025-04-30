using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication2.Utility
{
    public class EmailSender : IEmailSender
    {

        public Task SendEmailAsync(string email, string subject, string message)

        {

            var client = new SmtpClient("smtp.gmail.com", 587)

            {

                EnableSsl = true,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential("tariqshreem11@gmail.com", "uvfm kezp nong hacs")

            };

            return client.SendMailAsync(

            new MailMessage(from: "tariqshreem11@gmail.com",

            to: email,

            subject,

            message)


            {

                IsBodyHtml = true

            }

            );

        }






        }
}
