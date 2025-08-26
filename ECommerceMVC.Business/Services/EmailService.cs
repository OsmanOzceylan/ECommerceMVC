using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Business.Services
{
    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("osmanozceylan@gmail.com", "tkqa wfbt dtye ybgv"),
                EnableSsl = true
            };

            smtp.Send("osmanozceylan@gmail.com", to, subject, body);

        }
    }
}
