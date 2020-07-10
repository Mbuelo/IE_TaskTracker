using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IE_TaskTracker.Models;
using Microsoft.AspNet.Identity;

namespace IE_TaskTracker.Controllers
{
    public class _TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: _Tasks
        public ActionResult Index()
        {
            return View();
        }
        //FUNC: Get User
        private IEnumerable<_Task> Get_Tasks()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            return db.Tasks.ToList().Where(x => x.User == currentUser);
        }
        //GET: _TaskTable
        public ActionResult BuildTaskTable()
        {
            
            return PartialView("_TaskTable", Get_Tasks());
        }

        // GET: _Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _Task _Task = db.Tasks.Find(id);
            if (_Task == null)
            {
                return HttpNotFound();
            }
            return View(_Task);
        }

        // GET: _Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: _Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,IsDone")] _Task _Task)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);

                _Task.User = currentUser;
                db.Tasks.Add(_Task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_Task);
        }
        //POST: AJAX CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Description")] _Task _Task)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);

                _Task.User = currentUser;
                _Task.IsDone = false;
                db.Tasks.Add(_Task);
                db.SaveChanges();
            }

            return PartialView("_TaskTable", Get_Tasks());
        }

        // GET: _Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _Task _Task = db.Tasks.Find(id);
            if (_Task == null)
            {
                return HttpNotFound();
            }
            return View(_Task);
        }

        // POST: _Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDone")] _Task _Task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(_Task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_Task);
        }

        //CHECKBOX_HANDLER
        [HttpPost]
        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _Task _Task = db.Tasks.Find(id);
            if (_Task == null)
            {
                return HttpNotFound();
            }
            else
            {
                _Task.IsDone = value;
                db.Entry(_Task).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_TaskTable", Get_Tasks());
            }
            
        }

        // GET: _Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _Task _Task = db.Tasks.Find(id);
            if (_Task == null)
            {
                return HttpNotFound();
            }
            return View(_Task);
        }

        // POST: _Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Task _Task = db.Tasks.Find(id);
            db.Tasks.Remove(_Task);
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
