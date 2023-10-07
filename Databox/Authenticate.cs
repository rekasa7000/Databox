using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Databox
{
    class Authenticate
    {         
        public void Email(string tmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("databox.scheduler@gmail.com");
                mail.To.Add(tmail);
                mail.Subject = "Code Generated";
                mail.Body = "Enter this code: "+ code;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("databox.scheduler@gmail.com", "rlnkajyhsjysudas");
                smtp.Send(mail);
                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
