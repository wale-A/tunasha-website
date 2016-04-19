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

namespace TunashaProjects.Controllers
{
    public class ForumController : Controller
    {
        private DataContext db = new DataContext();

        // GET: List of Questions
        public ActionResult Index(string searchQuery, int? page)
        {
            //searchQuery = searchQuery.ToLower();
            List<Question> listOfQuestions = new List<Question>();
            var list = db.Question.OrderByDescending(x => x.Date);
            if (searchQuery != null)
            {
                page = 1;
                foreach (var question in list)
                {
                    if (question.Text.ContainsForSearch(searchQuery))
                    {
                        listOfQuestions.Add(question);
                    }
                }
            }
            else
                listOfQuestions = list.ToList();
            
            ViewBag.CurrentFilter = searchQuery;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listOfQuestions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel question)
        {
            if (ModelState.IsValid)
            {
                Question q = new Question()
                {
                    Text = question.Text,
                    Email = question.Email,
                    Name = question.Name,
                    Date = DateTime.Now
                };
                db.Question.Add(q);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Text,Name,Email,Date")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Question.Find(id);
            db.Question.Remove(question);
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
