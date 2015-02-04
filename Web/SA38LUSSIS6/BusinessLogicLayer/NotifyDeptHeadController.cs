using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace BusinessLogicLayer
{
    public class NotifyDeptHeadController
    {
        //public void notifyHead(String rID, String rDate, String eName, String dHead, String bText)

        //public void notifyHead(String rID, String rDate, String eName, String dHead, String bText)
        //{
        //    MailMessage m = new MailMessage("a0120516@nus.edu.sg", dHead);
        //    m.Subject = "New Stationery Request from " + eName;
        //    m.Body = bText;
        //    SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
        //    c.Send(m);
        //}

        public void emailDeptHead(String eName, String dHead, String bText)
        {
            MailMessage m = new MailMessage("a0120516@nus.edu.sg", dHead);
            m.Subject = "New Stationery Request from " + eName;
            m.Body = bText;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("lynx.iss.nus.edu.sg");
            smtp.Send(m);

            //MailMessage m = new MailMessage("sa38adteam6@gmail.com", dHead);
            //m.Subject = "New Stationery Request from " + eName;
            //m.Body = bText;
            //m.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.EnableSsl = true;
            //NetworkCredential NetworkCred = new NetworkCredential("sa38adteam6@gmail.com", "keepsmiling!");
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = NetworkCred;
            //smtp.Port = 465;
            //smtp.Send(m);
        }
    }
}
