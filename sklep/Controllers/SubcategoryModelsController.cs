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
    public class SubcategoryModelsController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: SubcategoryModels
        public ActionResult Index()
        {
            var subcategory = db.Subcategory.Include(s => s.category);
            return View(subcategory.ToList());
        }

        // GET: SubcategoryModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryModel subcategoryModel = db.Subcategory.Find(id);
            if (subcategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(subcategoryModel);
        }

        // GET: SubcategoryModels/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
            return View();
        }

        // POST: SubcategoryModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID")] SubcategoryModel subcategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Subcategory.Add(subcategoryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", subcategoryModel.CategoryID);
            return View(subcategoryModel);
        }

        // GET: SubcategoryModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryModel subcategoryModel = db.Subcategory.Find(id);
            if (subcategoryModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", subcategoryModel.CategoryID);
            return View(subcategoryModel);
        }

        // POST: SubcategoryModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID")] SubcategoryModel subcategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", subcategoryModel.CategoryID);
            return View(subcategoryModel);
        }

        // GET: SubcategoryModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryModel subcategoryModel = db.Subcategory.Find(id);
            if (subcategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(subcategoryModel);
        }

        // POST: SubcategoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubcategoryModel subcategoryModel = db.Subcategory.Find(id);
            db.Subcategory.Remove(subcategoryModel);
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
