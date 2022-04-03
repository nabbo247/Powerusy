using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public bool sendMail(string destinationEmail, string sourceEmail, string ccEmailAddress, string body, string subject, string filename)
        {
            bool status = false;
            try
            {
                //MailMessage msgMail = new MailMessage();
                MailMessage myMessage = new MailMessage();
                myMessage.From = new MailAddress(sourceEmail, "NJCPENSION");
                myMessage.To.Add(destinationEmail);
                if (!string.IsNullOrEmpty(ccEmailAddress))
                    myMessage.CC.Add(ccEmailAddress);

                myMessage.Subject = subject;
                myMessage.IsBodyHtml = true;
                myMessage.Body = body;
                string adUser = ConfigurationManager.AppSettings["smtpusername"].ToString();
                string adPwd = ConfigurationManager.AppSettings["smtppassword"].ToString();
                SmtpClient mySmtpClient = new SmtpClient();
                System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential(adUser, adPwd);
                mySmtpClient.Host = ConfigurationManager.AppSettings["MAIL_HOST"].ToString();
                //"plesk2500.is.cc";
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = myCredential;
                mySmtpClient.ServicePoint.MaxIdleTime = 1;
                mySmtpClient.Port = 587;
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Send(myMessage);

                myMessage.Dispose();

                status = true;
            }
            catch (SmtpFailedRecipientException smtpExc)
            {
                //ErrorLog log = new ErrorLog("<smtpExc Error>: " + smtpExc);
            }

            return status;
        }

    }
}
