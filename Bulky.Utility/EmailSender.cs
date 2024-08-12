using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            /*using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("developeracount@gmail.com");
                mail.Subject = subject;
                mail.To.Add(new MailAddress(email));
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("developeracount@gmaiil.com", "This is a false ");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }*/
            return Task.CompletedTask;
        }

    }
}
