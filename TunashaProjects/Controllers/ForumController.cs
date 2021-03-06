﻿using System;
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
    [Authorize]
    public class ForumController : Controller
    {
        private DataContext db = new DataContext();

        // GET: List of Questions
        [AllowAnonymous]
        public ActionResult Index(string searchQuery, int? page)
        {
            //searchQuery = searchQuery.ToLower();
            List<Question> listOfQuestions = new List<Question>();
            var list = db.Questions.OrderByDescending(x => x.Date);
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

        [AllowAnonymous]
        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Forum/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[AllowAnonymous]
        //[HttpPost, ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(QuestionViewModel question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Question q = new Question()
        //        {
        //            Text = question.Text,
        //            Email = question.Email,
        //            Name = question.Name,
        //            Date = DateTime.Now
        //        };
        //        db.Questions.Add(q);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(question);
        //}

        [AllowAnonymous]
        [HttpPost, ValidateInput(false), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQUestion(string name, string email, string question)
        {
            if (ModelState.IsValid)
            {
                Question q = new Question()
                {
                    Text = question,
                    Email = email,
                    Name = name,
                    Date = DateTime.Now
                };
                db.Questions.Add(q);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Forum/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Question question = db.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(question);
        //}

        //// POST: Forum/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Text,Name,Email,Date")] Question question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(question).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(question);
        //}

        // GET: Forum/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Question question = db.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(question);
        //}

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReply(string reply, int? questionId)
        {
            if (questionId == null )
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Question question = db.Questions.Find(questionId);
            if (question == null)
                return HttpNotFound();

            User u = db.Users.SingleOrDefault(x => x.Name == User.Identity.Name);
            if (reply != "")
            {
                Reply r = new Models.Reply()
                {
                    Text = reply,
                    Date = DateTime.Now,
                    QuestionID = questionId.Value,
                    Name = u.Name,
                    UserID = u.UserID
                };

                db.Replies.Add(r);
                question.Replies.Add(r);
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = questionId.Value });
            }
            return RedirectToAction("Details", new { id = questionId.Value });
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
