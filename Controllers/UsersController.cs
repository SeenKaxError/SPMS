using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPatrol.Models;

namespace SafetyPatrol.Controllers
{ 
    public class UsersController : Controller
    {
        private UsersDbContext db = new UsersDbContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(db.users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            tbl_users tbl_users = db.users.Find(id);
            return View(tbl_users);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(tbl_users);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tbl_users);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
            tbl_users tbl_users = db.users.Find(id);
            return View(tbl_users);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_users);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            tbl_users tbl_users = db.users.Find(id);
            return View(tbl_users);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            tbl_users tbl_users = db.users.Find(id);
            db.users.Remove(tbl_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}