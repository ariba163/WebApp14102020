using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSastiServices.Models.ViewModel;
using WebAppSastiServices.Models.EntityManager;
using WebAppSastiServices.Models.DB;
using System.Web.Helpers;

namespace WebAppSastiServices.Controllers
{
    public class AccountController : Controller
    {
        private SastaServicesDBEntities db = new SastaServicesDBEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegisterView user)
        {
            if (ModelState.IsValid)
            {
                
                var isExist = UserManager.IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exist");
                    return View(user);
                }
                else
                {
                    StpUser stpUser = new StpUser();
                    stpUser.UserName = user.UserName;
                    stpUser.EmailID = user.EmailID;
                    stpUser.Password = Crypto.Hash(user.Password);
                    stpUser.Contact = user.Contact;
                    stpUser.Address = user.Address;
                    stpUser.CreatedDate = System.DateTime.Now;
                    stpUser.IsEmailVerified = false;
                    stpUser.IsEmailActive = false;
                    stpUser.ActivationCode = Guid.NewGuid();
                    db.StpUsers.Add(stpUser);
                    db.SaveChanges();
                }
            }
            else
            {

            }
                return View(user);
        }
    }
}