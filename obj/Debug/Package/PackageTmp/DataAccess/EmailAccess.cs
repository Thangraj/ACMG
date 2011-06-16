using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace ACMGAdmin.DataAccess
{
    public class EmailAccess
    {


        public string sendMail(string fromName, string toName, string theSMTPServer, string SMTPUserName, string SMTPPassword,string theSubject, string theMessage, bool useSSL,Int16 portNumber=25){
         try
            {
                MailMessage mm = new MailMessage(fromName, toName);
                mm.Subject = theSubject;
                mm.Body = theMessage;
                SmtpClient smtp = new SmtpClient(theSMTPServer, portNumber);//587 for gmail
                NetworkCredential myCred = new NetworkCredential(SMTPUserName, SMTPPassword);
                smtp.Credentials = myCred;
                if (useSSL == true) {smtp.EnableSsl = true; }//use true for gmail
                smtp.Send(mm);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
                
            }



        }
    }
}