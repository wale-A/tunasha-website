using System;
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

namespace TunashaProjects.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Posts
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Post.Include(x => x.PostedBy).ToList());
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel post, ICollection<HttpPostedFileBase> files, ICollection<string> imgText)
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


                for (int i = 0; i < files.Count; i++)
                {
                    var img = files.ElementAt(i);
                    if (img != null)
                    {
                        string imgName = Path.GetFileName(img.FileName);
                        var file = new PostedFile()
                        {
                            Text = imgText.ElementAt(i),
                            DateAdded = DateTime.Now,
                            FilePath = Path.Combine(Server.MapPath("~/Images/Posts"), Guid.NewGuid().ToString() + "_" + imgName)
                        };
                        img.SaveAs(file.FilePath);
                        db.File.Add(file);
                        p.Images.Add(file);
                    }
                }

                db.Post.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
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
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
            Post post = db.Post.Find(postID);
            Comment c = new Comment()
            {
                Text = comment,
                Name = name,
                Date = DateTime.Now,
                PostID = postID
            };
            db.Comment.Add(c);
            post.Comments.Add(c);
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = postID });
        }

        public ActionResult DeleteComment(int commentID, int postID)
        {
            var comment = db.Comment.Find(commentID);
            db.Entry(comment).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = postID });
        }
    }
}
