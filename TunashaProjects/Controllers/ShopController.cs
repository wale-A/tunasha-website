using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TunashaProjects.DAL;
using TunashaProjects.Models;
using TunashaProjects.Misc;
using PagedList;
using System.IO;

namespace TunashaProjects.Controllers
{
    [Authorize (Roles = "Admin")]
    public class ShopController : Controller
    {
        private DataContext db = new DataContext();

        [AllowAnonymous]
        // GET: Products
        public ActionResult Index(string searchQuery, int? page)
        {
            List<Product> listOfProducts = new List<Product>();
            var products = db.Products.Include(p => p.Image).OrderBy(x => x.Name).ToList();

            if (searchQuery != null && searchQuery != "")
            {
                page = 1;
                foreach (var product in products)
                    if (product.Name.ContainsForSearch(searchQuery))
                        listOfProducts.Add(product);
            }
            else
                listOfProducts = products.ToList();

            ViewBag.CurrentFilter = searchQuery;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            
            return View(listOfProducts.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(x => x.Image).SingleOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            //ViewBag.PostedFileID = new SelectList(db.Files, "ID", "Text");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductID,Name,PostedFileID,Price,Description")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PostedFileID = new SelectList(db.Files, "ID", "Text", product.PostedFileID);
        //    return View(product);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0 && image.ContentType.Contains("image"))
                {
                    try
                    {
                        var filePath = Guid.NewGuid().ToString("N") + "_" + Path.GetFileName(image.FileName);
                        string path = Path.Combine(HttpContext.Server.MapPath("~/Images/Products"), filePath);
                        image.SaveAs(path);

                        PostedFile file = new PostedFile
                        {
                            FilePath = filePath,
                            Text = Path.GetFileNameWithoutExtension(image.FileName),
                            DateAdded = DateTime.UtcNow
                        };
                        db.Files.Add(file);
                        Product p = new Product
                        {
                            Description = product.Description,
                            Name = product.Name,
                            PostedFileID = file.ID,
                            Price = product.Price,
                            Image = file
                        };
                        db.Products.Add(p);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostedFileID = new SelectList(db.Files, "ID", "Text", product.PostedFileID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,PostedFileID,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostedFileID = new SelectList(db.Files, "ID", "Text", product.PostedFileID);
            return View(product);
        }

        //// GET: Products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        [AllowAnonymous]
        public ActionResult AddToCart(int? id)
        {
            if (id != null)
            {
                Cart cart;
                if (Session["CartGUID"] == null)
                    Session["CartGUID"] = Guid.NewGuid().ToString("N");
                cart = new Cart
                {
                    Guid = Session["CartGUID"].ToString(),
                    ProductID = id.Value
                };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
