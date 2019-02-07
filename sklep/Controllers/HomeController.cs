using sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sklep.Controllers
{
    public class HomeController : Controller
    {
        private ShopContext db = new ShopContext();
        public ActionResult Index()
        {
            ViewBag.categories = db.Category.ToList();

            try
            {
                ViewBag.promotedProducts = db.PromotedProducts.ToList();
                ViewBag.promotedProductsFlag = true;
            }
            catch(Exception e)
            {
                ViewBag.promotedProductsFlag = false;
            }

            try
            {
                ViewBag.mainPromotion = db.MainPromotion.First();
                ViewBag.mainPromotionFlag = true;
            }
            catch (Exception e)
            {
                ViewBag.mainPromotionFlag = false;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}