﻿using System.Net;
using System.Net.Mail;

namespace Shopping_Tutarial.Areas.Admin.Reponsitory
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true, //bật bảo mật
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("demologin979@gmail.com", "qtuixzjxgpnetvrq")
            };

            return client.SendMailAsync(
                new MailMessage(from: "demologin979@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
