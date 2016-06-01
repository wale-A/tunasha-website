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
using PagedList;

namespace TunashaProjects.Controllers
{
    [AllowAnonymous]
    public class MyCartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MyCart
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["CartGUID"] == null)
                Session["CartGUID"] = Guid.NewGuid().ToString("N");

            var guid = Session["CartGUID"].ToString();
            var cart = db.Carts.Include(c => c.Product).Where(x => x.Guid == guid).ToList();
            return View(cart.ToPagedList(pageNumber, pageSize));
        }

        //// GET: MyCart/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        //// GET: MyCart/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
        //    return View();
        //}

        //// POST: MyCart/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Guid,ProductID")] Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Carts.Add(cart);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", cart.ProductID);
        //    return View(cart);
        //}

        //// GET: MyCart/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", cart.ProductID);
        //    return View(cart);
        //}

        //// POST: MyCart/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Guid,ProductID")] Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cart).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", cart.ProductID);
        //    return View(cart);
        //}

        // GET: MyCart/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        // POST: MyCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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

        //public ActionResult PlaceOrder()
        //{
        //    var guid = Session["CartGUID"].ToString();
        //    if (guid == null || guid == "")
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
        //    var cart = db.Carts.Where(x => x.Guid == guid).ToList() ;
        //    if (cart == null || cart.Count < 1)
        //        return HttpNotFound();
        //    else
        //    {

        //    }
        //    return View(cart);
        //}
    }
}
