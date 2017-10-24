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
    public class ClassificationController : Controller
    {
        private ClassificationDatabase db = new ClassificationDatabase();

        //
        // GET: /Classification/

        public ViewResult Index()
        {
            return View(db.Classification.ToList());
        }

        //
        // GET: /Classification/Details/5

        public ViewResult Details(int id)
        {
            tbl_classification tbl_classification = db.Classification.Find(id);
            return View(tbl_classification);
        }

        //
        // GET: /Classification/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Classification/Create

        [HttpPost]
        public ActionResult Create(tbl_classification tbl_classification)
        {
            if (ModelState.IsValid)
            {
                db.Classification.Add(tbl_classification);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tbl_classification);
        }
        
        //
        // GET: /Classification/Edit/5
 
        public ActionResult Edit(int id)
        {
            tbl_classification tbl_classification = db.Classification.Find(id);
            return View(tbl_classification);
        }

        //
        // POST: /Classification/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_classification tbl_classification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_classification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_classification);
        }

        //
        // GET: /Classification/Delete/5
 
        public ActionResult Delete(int id)
        {
            tbl_classification tbl_classification = db.Classification.Find(id);
            return View(tbl_classification);
        }

        //
        // POST: /Classification/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            tbl_classification tbl_classification = db.Classification.Find(id);
            db.Classification.Remove(tbl_classification);
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