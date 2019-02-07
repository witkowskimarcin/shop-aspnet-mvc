using sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sklep.Controllers
{
    [Authorize(Users = "admin@admin.pl")]
    public class AdminController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Orders
        public ActionResult Orders()
        {
            ViewBag.orders = db.Order.ToList();
            return View();
        }

        // GET: Admin/OrderDetails
        public ActionResult OrderDetails(int? orderID)
        {
            if (orderID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.order = db.Order.Where(c => c.ID == orderID).First();
            return View();
        }
    }
}