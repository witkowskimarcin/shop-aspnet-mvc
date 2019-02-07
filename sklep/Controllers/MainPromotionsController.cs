using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sklep.Models;

namespace sklep.Controllers
{
    public class MainPromotionsController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: MainPromotions
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Index()
        {
            var mainPromotion = db.MainPromotion.Include(m => m.product);
            return View(mainPromotion.ToList());
        }

        // GET: MainPromotions/Details/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPromotion mainPromotion = db.MainPromotion.Find(id);
            if (mainPromotion == null)
            {
                return HttpNotFound();
            }
            return View(mainPromotion);
        }

        // GET: MainPromotions/Create
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");
            return View();
        }

        // POST: MainPromotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,Code,Quantity,Left")] MainPromotion mainPromotion)
        {
            if (ModelState.IsValid)
            {
                db.MainPromotion.Add(mainPromotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", mainPromotion.ProductID);
            return View(mainPromotion);
        }

        // GET: MainPromotions/Edit/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPromotion mainPromotion = db.MainPromotion.Find(id);
            if (mainPromotion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", mainPromotion.ProductID);
            return View(mainPromotion);
        }

        // POST: MainPromotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,Code,Quantity,Left")] MainPromotion mainPromotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainPromotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", mainPromotion.ProductID);
            return View(mainPromotion);
        }

        // GET: MainPromotions/Delete/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPromotion mainPromotion = db.MainPromotion.Find(id);
            if (mainPromotion == null)
            {
                return HttpNotFound();
            }
            return View(mainPromotion);
        }

        // POST: MainPromotions/Delete/5
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainPromotion mainPromotion = db.MainPromotion.Find(id);
            db.MainPromotion.Remove(mainPromotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
