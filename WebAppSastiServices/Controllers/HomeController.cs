using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSastiServices.Models.DB;
using System.IO;
using System.Net;
using System.Net.Mail;
using WebAppSastiServices.Models;

namespace WebAppSastiServices.Controllers
{

    public class HomeController : Controller
    {
        SastaServicesDBEntities db = new SastaServicesDBEntities();
        // GET: Home


    [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult pvGetInTouch()
        {
            return PartialView();
        }
        
        public PartialViewResult pvQuickCall()
        {
            return PartialView();
        }

        [HttpPost]
        public RedirectResult pvQuickCall(string Name,String Contact)
        {

            STPQuickCall Qcall = new STPQuickCall() 
            {
                Name = Name,
                Contact = Contact
            };

            db.STPQuickCalls.Add(Qcall);
            db.SaveChanges();

            return Redirect(Url.Action("Index", "Home"));
        }

        
        public ActionResult CustomerOrder(int? serviceTypeId)
        {
            ViewBag.ServiceType = new SelectList(db.STPServiceTypes.Where(s => s.ID == serviceTypeId), "ID", "ServiceTypeName");
            ViewBag.FuelType = new SelectList(db.STPServicesFuelTypes.Where(f => f.STPServiceTypeID == serviceTypeId), "ID", "Options");
            ViewBag.UnitType = new SelectList(db.STPServicesUnitTypes.Where(u => u.STPServiceTypeID == serviceTypeId), "ID", "Options");
            ViewBag.preferredTime = new SelectList(db.STPPrefferedTimes, "ID", "TimeRange");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerOrder(TRNCustomerOrder model)
        {
            if (ModelState.IsValid)
            {

                TRNCustomerOrder o = new TRNCustomerOrder()
                {
                    CustomerName = model.CustomerName,
                    Contact = model.Contact,
                    Address = model.Address,
                    OrderDescription = model.OrderDescription,
                    preferredDate = model.preferredDate,
                    preferredTimeID = model.preferredTimeID,
                    FuelTypeId = model.FuelTypeId,
                    UnitTypeId = model.UnitTypeId,
                    ServiceTypeId = model.ServiceTypeId,
                    CreateOn = System.DateTime.Now,
                    OrderStatusId = 6


                };
                db.TRNCustomerOrders.Add(o);
                db.SaveChanges();
            }

            return Redirect(Url.Action("Index", "Home"));
        }


        [HttpPost]
        public ActionResult Index(string Name,
                                    string Email,
                                    string Subject,
                                    string Phone,
                                    string Message)
        {

            if (Name != null && Name != "" && Email != null && Email != "" && Phone != null && Phone != "" && Message != null && Message != "")
            {
                string RequestNumber = DateTime.Now.Day.ToString() +
                                   DateTime.Now.Month.ToString() +
                                   DateTime.Now.Year.ToString() +
                                   DateTime.Now.Hour.ToString() +
                                   DateTime.Now.Minute.ToString() +
                                   DateTime.Now.Second.ToString();


                SendEmail("sastiService123@gmail.com",
                          "aribajawed163@gmail.com", //Email of customer support employee
                          Email,
                          "Email Received by " + Name,
                          Message,
                          "smtp.gmail.com",
                          587,
                          "sastiService123456",
                          RequestNumber,
                          true);

                // Entry into the Database

                //CustomerSupport cp = new CustomerSupport()
                //{
                //    FullName = txtName,
                //    Mobile = txtMobile,
                //    Email = Designation.EmailAddress,
                //    Message = txtMessage,
                //    RequestNumber = RequestNumber

                //};

                //db.CustomerSupports.Add(cp);
                //db.SaveChanges();

            }

            return View();
        }

        public void SendEmail(string emailFromAddress,
                                     string emailToAddress,
                                     string emailUser,
                                     string subject,
                                     string body,
                                     string smtpAddress,
                                     int portNumber,
                                     string password,
                                     string RequestNumber,
                                     bool enableSSL)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.CC.Add(emailUser);
                mail.Subject = subject;
                string strBody = "<html><head><Title> Sasti Service </Title></head><body bgcolor='#ccc'>  <h1>Email Notification from Customer</h1><br /> <p>The message has been received by $$$emailUser$$$</p><br /> <h3>Request Number: $$$RequestNumber$$$</h3><br />  <h3>Message: $$$body$$$</h3><br /></body></html>";
                strBody = strBody.Replace("$$$emailUser$$$", emailUser);
                strBody = strBody.Replace("$$$RequestNumber$$$", RequestNumber);
                strBody = strBody.Replace("$$$body$$$", body);
                mail.Body = strBody;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);

                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);



                }
            }
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }





        //[HttpPost]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}
        // [HttpGet]
        public PartialViewResult PVservices()

        {
            var Services = (from d in db.STPServiceTypes
                            orderby d.ID descending
                            select d).Take(5).ToList();

            return PartialView(Services);

        }

    }
}