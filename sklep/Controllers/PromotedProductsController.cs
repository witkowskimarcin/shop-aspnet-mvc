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
    [Authorize(Users = "admin@admin.pl")]
    public class PromotedProductsController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: PromotedProducts
        public ActionResult Index()
        {
            var promotedProducts = db.PromotedProducts.Include(p => p.product);
            return View(promotedProducts.ToList());
        }

        // GET: PromotedProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotedProducts promotedProducts = db.PromotedProducts.Find(id);
            if (promotedProducts == null)
            {
                return HttpNotFound();
            }
            return View(promotedProducts);
        }

        // GET: PromotedProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");
            return View();
        }

        // POST: PromotedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID")] PromotedProducts promotedProducts)
        {
            if (ModelState.IsValid)
            {
                db.PromotedProducts.Add(promotedProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", promotedProducts.ProductID);
            return View(promotedProducts);
        }

        // GET: PromotedProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotedProducts promotedProducts = db.PromotedProducts.Find(id);
            if (promotedProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", promotedProducts.ProductID);
            return View(promotedProducts);
        }

        // POST: PromotedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID")] PromotedProducts promotedProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotedProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", promotedProducts.ProductID);
            return View(promotedProducts);
        }

        // GET: PromotedProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotedProducts promotedProducts = db.PromotedProducts.Find(id);
            if (promotedProducts == null)
            {
                return HttpNotFound();
            }
            return View(promotedProducts);
        }

        // POST: PromotedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotedProducts promotedProducts = db.PromotedProducts.Find(id);
            db.PromotedProducts.Remove(promotedProducts);
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
