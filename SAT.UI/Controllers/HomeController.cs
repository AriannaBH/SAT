using System;
using System.Web.Mvc;
using SAT.UI.Models;
using System.Net;
using System.Net.Mail;

namespace SAT.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //Post - The user submission of the Get View (form in that view)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            
            if (!ModelState.IsValid)
            {
                
                return View(cvm);
            }
            //The code below only executes if the ModelState.IsValid because of the check above.

            //MailMessage object - what sends the email - System.Net.Mail
            //build the Message - what we see when we receive the email
            string message = $"You have received an email from {cvm.Name} with a subject {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage(
                //FROM
                "no-reply@.com",
                //TO - this assumes forwarding by the host...until smarterasp.net fixes the issue of blocking normal email address (outlook.com, gmail.com), we are going to hard code the email that we want this message sent to
                "",
                //SUBJECT
                cvm.Subject,
                //BODY
                message
                );

            //MailMessage Properties
            //Allow HTML formatting in the email (message has HTML in it)
            mm.IsBodyHtml = true;

            //If you want to set hight priority on these messages, place the following
            mm.Priority = MailPriority.High;

            //Respond to the sender's email rather than our own SMTP Client (webmail)
            mm.ReplyToList.Add(cvm.Email);

            //Assemble the SMTPClient object - This is the information from the Host (smarterASP.net). This allows the message to actually be sent.
            SmtpClient client = new SmtpClient("mail.");

            //Client credentials (userName and password for the email user you set up in the host)
            client.Credentials = new NetworkCredential("", "");

            client.Port = 8889;
            //It is possible that the mailserver is down or we may have configuration issues, so we want to encapsulate our code in a try/catch
            try
            {
                //attempt to send the email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be completed at this time. Please try again later.<br/>Error Message:<br/>{ex.StackTrace}";
                return View(cvm);
                //return the view with the entire message so that users can copy/paste later on
            }
            //If all goes well and the message is sent, return a seperate view that displays a receipt of their inputs and confirms the message has been sent
            return View("EmailConfirmation", cvm);
        }
    }
}
