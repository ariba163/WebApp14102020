using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppSastiServices.Models.DB;

namespace WebAppSastiServices.Models.EntityManager
{
    
    public static class UserManager
    {
        //private SastaServicesDBEntities db=new SastaServicesDBEntities();

        public static bool IsEmailExist(string emailID)
        {
            using (SastaServicesDBEntities db = new SastaServicesDBEntities())
            {
                var v = db.StpUsers.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }
    }
}