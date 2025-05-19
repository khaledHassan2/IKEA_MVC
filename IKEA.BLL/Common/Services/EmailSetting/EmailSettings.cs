using IKEA.DAL.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.EmailSetting
{
   public class EmailSettings:IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            //Sender- Reciver
            //Reciver= khaledamalb22@gmail.com => User Who Try To reset Password
            client.Credentials = new NetworkCredential("khaledamalb22@gmail.com", "tnhtwpoddklhypkg");//Generate Password
            client.Send("khaledamalb22@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
