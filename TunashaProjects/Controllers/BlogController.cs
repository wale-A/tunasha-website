﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TunashaProjects.DAL;
using TunashaProjects.Models;
using PagedList;

namespace TunashaProjects.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Posts
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var posts = db.Posts.Include(x => x.PostedBy).ToList();
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Images).Include(p => p.Comments).SingleOrDefault(p => p.PostID == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost, ValidateInput(false)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel post/*, ICollection<HttpPostedFileBase> files, ICollection<string> imgText*/)
        {
            if (ModelState.IsValid)
            {
                string s = new Guid().ToString();
                Post p = new Post()
                {
                    Content = post.Content,
                    Title = post.Title,
                    Date = DateTime.Now,
                    //fix this
                    UserID = 1
                };


                //for (int i = 0; i < files.Count; i++)
                //{
                //    var img = files.ElementAt(i);
                //    if (img != null)
                //    {
                //        string imgName = Path.GetFileName(img.FileName);
                //        var file = new PostedFile()
                //        {
                //            Text = imgText.ElementAt(i),
                //            DateAdded = DateTime.Now,
                //            FilePath = Guid.NewGuid().ToString() + "_" + imgName
                //            //FilePath = Path.Combine(@"~\Images\Blog", Guid.NewGuid().ToString() + "_" + imgName)
                //        };
                //        img.SaveAs(Server.MapPath("~/Images/Blog/" + file.FilePath));
                //        db.Files.Add(file);
                //        p.Images.Add(file);
                //    }
                //}

                db.Posts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

         //GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Title,Content,Date,UserID")] Post post)
        {
            if (ModelState.IsValid)
            {
                var p = db.Posts.Find(post.PostID);
                p.Title = post.Title;
                p.Content = post.Content;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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

        //public ActionResult AddComment(CommentViewModel comment, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Post.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // POST: Posts/Delete/5
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string name, string comment, int postID)
        {
            Post post = db.Posts.Find(postID);
            Comment c = new Comment()
            {
                Text = comment,
                Name = name,
                Date = DateTime.Now,
                PostID = postID
            };
            db.Comments.Add(c);
            post.Comments.Add(c);
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = postID });
        }

        public ActionResult Comment(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = db.Posts.Find(id);
            post.DisableComment = !(post.DisableComment);
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteComment(int? commentID, int postID)
        {
            var comment = db.Comments.Find(commentID);
            db.Entry(comment).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = postID });
        }
    }
}
