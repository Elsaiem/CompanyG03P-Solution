using CompanyG03DAL.Models;
using System.Net;
using System.Net.Mail;

namespace CompanyG03PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            //mail server : gmail.com
            //smtp: simple mail transfer protocol
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("y.saiem2111@gmail.com", "ktgtzzfzzciqqwwo");

            client.Send("y.saiem2111@gmail.com",email.To,email.Subject,email.Body);


        }


    }
}
