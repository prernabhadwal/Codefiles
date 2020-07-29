using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("UpdateonEmail")]
        public ActionResult UpdateonEmail1()
        {

            return View();
        }


        //update tracker if email has been sent
        [HttpPost]
        [ActionName("UpdateonEmail")]
        public ActionResult UpdateonEmail11(emaildata data1)
        {emailEntities obj=new emailEntities();
            emaildata oo=obj.emaildatas.Where(s=>s.id==data1.id).FirstOrDefault();
            oo.tracker=1;
            obj.SaveChanges();

            return View();
        }
        //send email method
        public ActionResult About()
        {
            emailEntities obj =new emailEntities();
            List<string> emails=obj.emaildatas.Select(s=>s.email).ToList();
            foreach(var email in emails) { 
            var frommail= new MailAddress("prerna2040@gmail.com");
            var tomail= new MailAddress(email);
            var fromemailpassword="password";
            string subject="Link for testing";
                int id=obj.emaildatas.Where(s=>s.email==email).Select(d=>d.id).FirstOrDefault();
            string body= "https://localhost:44374/"+ id ;
            var smtp =new SmtpClient
            {
                Host="smtp.gmail.com",
            Port=587,
            EnableSsl=true,
            DeliveryMethod=SmtpDeliveryMethod.Network,
            UseDefaultCredentials=false,
            Credentials=new NetworkCredential(frommail.Address,fromemailpassword)

            };

            using(var message = new MailMessage(frommail,tomail)
            {
                Subject=subject,
                Body=body,
                IsBodyHtml=true
            })
smtp.Send(message);

            }

            ViewBag.Message = "email sent.";

            return View();
        }

        //if email has not been opened or link not clicked send email again
        public ActionResult Contact()
        {
            emailEntities obj= new emailEntities();
            List<emaildata> hh= obj.emaildatas.Where(s=>s.tracker!=1).ToList();

          
            foreach (var x in hh)
            {
                var frommail = new MailAddress("prerna2040@gmail.com");
                var tomail = new MailAddress(x.email);
                var fromemailpassword = "password";
                string subject = "Link for testing";
                int id = obj.emaildatas.Where(s => s.email == x.email).Select(d => d.id).FirstOrDefault();
                string body = "https://localhost:44374/" + id;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(frommail.Address, fromemailpassword)

                };

                using (var message = new MailMessage(frommail, tomail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }

            ViewBag.Message = "email sent again";

            return View();
        }
    }
}