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
    [Authorize(Roles = "User")]
    public class MyOrderController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MyOrders
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var userEmail = User.Identity.Name;
            int userID = db.Users.SingleOrDefault(x => x.Email == userEmail).UserID;
            var orderDetails = db.OrderDetails.Include(o => o.Product).Where(x => x.UserID == userID).OrderByDescending(x => x.Date);
            return View(orderDetails.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: MyOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Include(x => x.Product).SingleOrDefault(x => x.ID == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Orders/Create
        public ActionResult NewOrder()
        {
            if (Session["CartGUID"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var guid = Session["CartGUID"].ToString();
            var cart = db.Carts.Where(x => x.Guid == guid).ToList();
            if (cart == null || cart.Count < 1)
                return HttpNotFound();
            return View();
        }

        // POST: Orders/NewOrder
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var guid = Session["CartGUID"].ToString();
                var cart = db.Carts.Where(x => x.Guid == guid).ToList();
                var user = db.Users.SingleOrDefault(x => x.Email == User.Identity.Name);
                Order newOrder = new Order
                {
                    CustomerAddress = order.CustomerAddress,
                    CustomerAddress2 = order.CustomerAddress2,
                    CustomerName = order.CustomerName,
                    CustomerPhone = order.CustomerPhone,
                    CustomerPhone2 = order.CustomerPhone2,
                    Date = DateTime.UtcNow,
                    User = user,
                    UserID = user.UserID
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();
                foreach (var item in cart)
                {
                    OrderDetail od = new OrderDetail
                    {
                        ProductID = item.ProductID,
                        OrderID = newOrder.ID,
                        Amount = item.Product.Price,
                        Date = DateTime.UtcNow,
                        TransactionNumber = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString() + DateTime.UtcNow.Day.ToString() + user.UserID.ToString("d3") + newOrder.ID.ToString("d3") + item.ProductID.ToString("d3"),
                        UserID = user.UserID
                    };
                    db.OrderDetails.Add(od);
                }
                db.SaveChanges();
                Session["CartGUID"] = null;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //// GET: MyOrders/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
        //    return View();
        //}

        // POST: MyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,ProductID,OrderID,Amount,Date")] OrderDetail orderDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.OrderDetails.Add(orderDetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderDetail.ProductID);
        //    return View(orderDetail);
        //}

        //// GET: MyOrders/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderDetail orderDetail = db.OrderDetails.Find(id);
        //    if (orderDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderDetail.ProductID);
        //    return View(orderDetail);
        //}

        //// POST: MyOrders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,ProductID,OrderID,Amount,Date")] OrderDetail orderDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(orderDetail).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderDetail.ProductID);
        //    return View(orderDetail);
        //}

        //// GET: MyOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderDetail orderDetail = db.OrderDetails.Find(id);
        //    if (orderDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderDetail);
        //}

        //// POST: MyOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    OrderDetail orderDetail = db.OrderDetails.Find(id);
        //    db.OrderDetails.Remove(orderDetail);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
