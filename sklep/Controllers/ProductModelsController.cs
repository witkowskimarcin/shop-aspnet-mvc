using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sklep.Models;
using System.IO;
using System.Collections.ObjectModel;
using sklep.Models.Forms;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace sklep.Controllers
{
    public class ProductModelsController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: ProductModels/Products/5
        public ActionResult Products(int? subCatID)
        {
            if (subCatID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var products = db.Product.Include(c => c.subcategory).Where(c => c.SubcategoryID == subCatID);
                if (products == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SubCatID = subCatID;
                ViewBag.exist = true;
                return View(products.ToList());
            }
            catch (Exception e)
            {
                ViewBag.exist = false;
                return View();
            }
        }

        // GET: ProductModels/Product/5
        public ActionResult Product(int? prodID)
        {
            if (prodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var product = db.Product.Where(c => c.ID == prodID);
                if (product == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ProdID = prodID;
                ViewBag.exist = true;
                ViewBag.product = product.First();
                ViewBag.images = product.First().images.ToList();
                ViewBag.numberOfImages = (int) product.First().images.Count;
                List<int> iteration = new List<int>();
                for(int i=1; i<= product.First().images.Count; ++i)
                {
                    iteration.Add(i);
                }
                ViewBag.iteration = iteration;
                return View(product.First());
            }
            catch (Exception e)
            {
                ViewBag.exist = false;
                return View();
            }
        }

        // GET: ProductModels/Cart
        public ActionResult Cart()
        {
            return View();
        }

        // POST: ProductModels/CleanCart
        [HttpPost]
        public ActionResult CleanCart()
        {
            Session["cart"] = null;
            return RedirectToAction("Cart", "ProductModels");
        }

        // POST: ProductModels/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int? prodID)
        {
            if (prodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Product.Where(c => c.ID == prodID).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            CartModel cart = null;
            if (Session["cart"] == null)
            {
                cart = new CartModel();
                cart.addProduct(product);
                Session["cart"] = cart;
            }
            else
            {
                cart = (CartModel) Session["cart"];
                cart.addProduct(product);
                Session["cart"] = cart;
            }

            return RedirectToAction("Product", "ProductModels", new { prodID = prodID });
        }

        // POST: ProductModels/CartMinus/5
        [HttpPost]
        public ActionResult CartMinus(int? prodID)
        {
            if (prodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Product.Where(c => c.ID == prodID).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            CartModel cart = null;
            if (Session["cart"] == null)
            {

            }
            else
            {
                cart = (CartModel)Session["cart"];
                cart.removeProduct(product);

                if(cart.getQuantity()==0)
                {
                    Session["cart"] = null;
                }
                else
                {
                    Session["cart"] = cart;
                }
            }

            return RedirectToAction("Cart", "ProductModels");
        }

        // POST: ProductModels/CartPlus/5
        [HttpPost]
        public ActionResult CartPlus(int? prodID)
        {
            if (prodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Product.Where(c => c.ID == prodID).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            CartModel cart = null;
            if (Session["cart"] == null)
            {

            }
            else
            {
                cart = (CartModel)Session["cart"];
                cart.addProduct(product);
                Session["cart"] = cart;
            }

            return RedirectToAction("Cart", "ProductModels");
        }

        // GET: ProductModels/Order
        [Authorize]
        public ActionResult Order()
        {
            CartModel cart = null;
            if (Session["cart"] == null)
            {
                ViewBag.flag = false;
            }
            else
            {
                cart = (CartModel)Session["cart"];
                if(cart.getQuantity()>0)
                {
                    ViewBag.flag = true;

                    try
                    {
                        string query = "SELECT * FROM UserDetails WHERE Email = @email";
                        UserDetails ud = db.UserDetails.SqlQuery(query, new SqlParameter("@email", User.Identity.GetUserName())).First();
                        //UserDetails ud = db.UserDetails.Where(c => c.Email == User.Identity.GetUserName()).First();
                        ViewBag.userDetails = ud;
                    }
                    catch (Exception e)
                    {
                        ViewBag.userDetails = new UserDetails();
                    }
                }
                else
                {
                    ViewBag.flag = false;
                }
            }
            
            return View();
        }

        // POST: ProductModels/Order
        [Authorize]
        [HttpPost]
        public ActionResult Order(OrderForm orderForm)
        {
            Session["order"] = orderForm;

            return RedirectToAction("OrderSum");
        }

        // GET: ProductModels/OrderSum
        [Authorize]
        public ActionResult OrderSum()
        {
            return View();
        }

        // POST: ProductModels/OrderSumPost
        [Authorize]
        [HttpPost]
        public ActionResult OrderSumPost()
        {
            OrderForm orderForm = (OrderForm) Session["order"];
            CartModel cart = (CartModel)Session["cart"];

            OrderModel order = new OrderModel();
            order.Firstname = orderForm.Firstname;
            order.Lastname = orderForm.Lastname;
            order.Locality = orderForm.Locality;
            order.Street = orderForm.Street;
            order.Zipcode = orderForm.Zipcode;
            order.Phone = orderForm.Phone;
            order.Shipment = orderForm.Shipment;
            order.Date = DateTime.Now;
            order.Description = orderForm.Description;
            order.orderDetails = new Collection<OrderDetailModel>();

            UserDetails ud = null;

            if (orderForm.Remember == true)
            {
                try
                {
                    string query = "SELECT * FROM UserDetails WHERE Email = @email";
                    ud = db.UserDetails.SqlQuery(query, new SqlParameter("@email", User.Identity.GetUserName())).First();
                    ud.Firstname = orderForm.Firstname;
                    ud.Lastname = orderForm.Lastname;
                    ud.Locality = orderForm.Locality;
                    ud.Street = orderForm.Street;
                    ud.Zipcode = orderForm.Zipcode;
                    ud.Phone = orderForm.Phone;

                    db.Entry(ud).State = EntityState.Modified;
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    ud = new UserDetails();

                    ud.Firstname = orderForm.Firstname;
                    ud.Lastname = orderForm.Lastname;
                    ud.Locality = orderForm.Locality;
                    ud.Street = orderForm.Street;
                    ud.Zipcode = orderForm.Zipcode;
                    ud.Phone = orderForm.Phone;
                    ud.Email = User.Identity.GetUserName();

                    db.UserDetails.Add(ud);

                    db.SaveChanges();
                }
            }
            else
            {
                try
                {
                    string query = "SELECT * FROM UserDetails WHERE Email = @email";
                    ud = db.UserDetails.SqlQuery(query, new SqlParameter("@email", User.Identity.GetUserName())).First();
                }
                catch (Exception e)
                {
                    ud = new UserDetails();

                    ud.Firstname = orderForm.Firstname;
                    ud.Lastname = orderForm.Lastname;
                    ud.Locality = orderForm.Locality;
                    ud.Street = orderForm.Street;
                    ud.Zipcode = orderForm.Zipcode;
                    ud.Phone = orderForm.Phone;
                    ud.Email = User.Identity.GetUserName();

                    db.UserDetails.Add(ud);

                    db.SaveChanges();
                }
            }

            foreach (var item in cart.products)
            {
                ProductModel p = item.Key;
                int q = item.Value;

                if (p.Quantity >= q)
                {
                    p.Quantity -= q;
                    db.Entry(p).State = EntityState.Modified;
                }
                else
                {
                    Session["order"] = null;
                    Session["cart"] = null;

                    return RedirectToAction("OrderResultUnsuccessful");
                }

                OrderDetailModel od = new OrderDetailModel();
                od.product = p;
                od.Quantity = q;

                db.OrderDetail.Add(od);

                order.orderDetails.Add(od);
               
            }

            order.user = ud;
            db.Order.Add(order);
            db.SaveChanges();

            Session["order"] = null;
            Session["cart"] = null;

            return RedirectToAction("OrderResult");
        }

        // GET: ProductModels/OrderResult
        [Authorize]
        public ActionResult OrderResult()
        {
            return View();
        }

        // GET: ProductModels/OrderResultUnsuccessful
        [Authorize]
        public ActionResult OrderResultUnsuccessful()
        {
            return View();
        }

        // GET: ProductModels
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.subcategory);
            return View(product.ToList());
        }

        // GET: ProductModels/Details/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Product.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = productModel;
            return View(productModel);
        }

        // GET: ProductModels/Create
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Create()
        {
            ViewBag.SubcategoryID = new SelectList(db.Subcategory, "ID", "Name");
            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,SubcategoryID,Name,Description,Price")] ProductModel productModel, HttpPostedFileBase files)
        public ActionResult Create(ProductAddForm form)
        {
            ImageModel imageModel = null;
            ProductModel productModel = new ProductModel();
            productModel.Description = form.Description;
            productModel.Name = form.Name;
            productModel.Price = form.Price;
            productModel.Quantity = form.Quantity;
            productModel.SubcategoryID = form.SubcategoryID;
            productModel.images = new Collection<ImageModel>();

            //ICollection < ImageModel > kk = new I
            for (int i = 0; i < form.files.Length; ++i)
            {
                if (form.files[i].ContentLength > 0)
                {
                    imageModel = new ImageModel();
                    byte[] pictureAsByte = new byte[form.files[i].ContentLength];
                    using (BinaryReader theReader = new BinaryReader(form.files[i].InputStream))
                    {
                        pictureAsByte = theReader.ReadBytes(form.files[i].ContentLength);
                    }
                    string pictureDataAsString = Convert.ToBase64String(pictureAsByte);
                    imageModel.image = pictureDataAsString;
                    db.Image.Add(imageModel);
                    db.SaveChanges();

                    productModel.images.Add(imageModel);
                }
            }
            db.Product.Add(productModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ProductModels/Edit/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Product.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubcategoryID = new SelectList(db.Subcategory, "ID", "Name", productModel.SubcategoryID);
            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubcategoryID,Name,Description,Price,Quantity")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubcategoryID = new SelectList(db.Subcategory, "ID", "Name", productModel.SubcategoryID);
            return View(productModel);
        }

        // GET: ProductModels/Delete/5
        [Authorize(Users = "admin@admin.pl")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Product.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = productModel;
            return View(productModel);
        }

        // POST: ProductModels/Delete/5
        [Authorize(Users = "admin@admin.pl")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productModel = db.Product.Find(id);
            if (productModel.images.Count > 0)
            {
                foreach (var item in productModel.images.ToList())
                {
                    db.Image.Remove(item);
                }
                db.SaveChanges();
            }
            db.Product.Remove(productModel);
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
