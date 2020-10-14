using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSastiServices.Models;
using WebAppSastiServices.Models.DB;
    

namespace WebAppSastiServices.Controllers
{
    public class AdminDashboardController : Controller
    {
        SastaServicesDBEntities db = new SastaServicesDBEntities();
        // GET: AdminDashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LatestActOrders()
        {
            db.Configuration.ProxyCreationEnabled = false;

            //---------Single Table Data---------
            //IList<TRNCustomerOrder> orders = db.TRNCustomerOrders.
            //    Where(s => s.STPStatu.Description == "Not Availed").
            //    ToList<TRNCustomerOrder>();
           

            var result = (from o in db.TRNCustomerOrders
                                         join t in db.STPPrefferedTimes on o.preferredTimeID equals t.ID
                                          join s in db.STPServiceTypes on o.ServiceTypeId equals s.ID
                                          join st in db.STPStatus on o.OrderStatusId equals st.ID
                          select new
                                          {
                                              OrderId = o.OrderId,
                                              CustomerName = o.CustomerName,
                                              Contact = o.Contact,
                                              Address = o.Address,
                                              Description = s.ServiceTypeName,
                                              TimeRange = t.TimeRange,
                                              preferredDate = o.preferredDate.ToString(),
                                              status = st.Description,
                                              CreateOn= o.CreateOn.ToString(),
                                              btns= " <div class='btn-group' role='group'><a type='button' class='btn btn-secondary text-light' onclick=OpenDetailForm(" + o.OrderId + ") id='Detail'><i class='fas fa-info-circle'></i> </a>" +
                                                    "<a type='button' class='btn btn-danger text-light' onclick=OpenDeleteForm(" + o.OrderId + ") id='Delete'><i class='fas fa-trash'></i> </a>" +
                                                    "<a type='button' class='btn btn-primary text-light' onclick=OpenEditForm(" + o.OrderId + ") id='Edit'><i class='far fa-edit'></i> </a></div>"

                                          }).ToList();


            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult OrderDetails(int? ID)
        {
            var details = (from d in db.TRNCustomerOrders
                           where (d.OrderId == ID)
                           select d).SingleOrDefault();

            return View(details);
        }
        public ActionResult OrderDelete(int? ID)
        {

            var details = (from d in db.TRNCustomerOrders
                           where (d.OrderId == ID)
                           select d).SingleOrDefault();

            return View(details);
        }
        [HttpPost]
        public ActionResult OrderDelete(string OrderId)
        {
            int ID = Convert.ToInt32(OrderId);
            var Order = (from d in db.TRNCustomerOrders
                         where (d.OrderId == ID)
                         select d).SingleOrDefault();
            db.TRNCustomerOrders.Remove(Order);
            db.SaveChanges();

            return Redirect(Url.Action("Index", "AdminDashboard"));
        }
        public ActionResult OrderEdit(int? ID)
        {
            var serviceID = (from d in db.TRNCustomerOrders where (d.OrderId == ID) select d.ServiceTypeId).FirstOrDefault();
            ViewBag.FuelType = new SelectList(db.STPServicesFuelTypes.Where(s=> s.STPServiceTypeID == serviceID), "ID", "Options");
            ViewBag.UnitType = new SelectList(db.STPServicesUnitTypes.Where(s => s.STPServiceTypeID == serviceID), "ID", "Options");
            ViewBag.preferredTime = new SelectList(db.STPPrefferedTimes, "ID", "TimeRange");
            ViewBag.status = new SelectList(db.STPStatus.Where(s=> s.STPStatusType.Description== "Orders"), "ID", "Description");


            var details = (from d in db.TRNCustomerOrders
                           where (d.OrderId == ID)
                           select d).SingleOrDefault();

            return View(details);
        }
        [HttpPost]
        public ActionResult OrderEdit(TRNCustomerOrder order)
        {


            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect(Url.Action("Index", "AdminDashboard"));
        }





        public ActionResult Email()
        {
            return View();
        }
        public ActionResult Vendor()
        {
            return View();
        }
        //public ActionResult Supplier()
        //{
        //    return View();
        //}
        public ActionResult AdminProfile()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LockScreen()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult InvoiceReport()
        {
            var result = (from o in db.TRNCustomerOrders
                          join t in db.STPPrefferedTimes on o.preferredTimeID equals t.ID
                          join s in db.STPServiceTypes on o.ServiceTypeId equals s.ID
                          join st in db.STPStatus on o.OrderStatusId equals st.ID
                          join f in db.STPServicesFuelTypes on o.FuelTypeId equals f.ID
                          join u in db.STPServicesUnitTypes on o.UnitTypeId equals u.ID
                          select new
                          {
                              OrderId = o.OrderId,
                              CustomerName = o.CustomerName,
                              Contact = o.Contact,
                              Address = o.Address,
                              Description = s.ServiceTypeName,
                              TimeRange = t.TimeRange,
                              preferredDate = o.preferredDate,
                              status = st.Description,
                              FuelType =f.Options,
                              unitType =u.Options
                          }).ToList();
            return View();
        }
        public ActionResult Maps()
        {
            return View();
        }
    }
}