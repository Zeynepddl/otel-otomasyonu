using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    public class EmailSender
    {
        private string username;
        private string password;
        public EmailSender(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void SifreKurtarmaMailiGonder(string aliciMail, string yeniSifre)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(this.username);
            mail.To.Add(new MailAddress(aliciMail));
            mail.Subject = "Sifre Kurtarma Maili";
            mail.Body = "Talebiniz doğrultusunda şifreniz sıfırlanmıştır. Yeni Şifreniz: " + yeniSifre;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(this.username, this.password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }
    }
}
