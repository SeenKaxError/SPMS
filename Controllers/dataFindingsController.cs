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
    public class dataFindingsController : Controller
    {
        private FindingsDatabase db = new FindingsDatabase();

        //
        // GET: /dataFindings/

        public ViewResult Index()
        {
             return View(db.dataFindings);
        }

        //
        // GET: /dataFindings/Details/5

        public ViewResult Details(int id)
        {
            tbl_dataFindings tbl_datafindings = db.dataFindings.Find(id);
            return View(tbl_datafindings);
        }

        //
        // GET: /dataFindings/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /dataFindings/Create

        [HttpPost]
        public ActionResult Create(tbl_dataFindings tbl_datafindings)
        {
            if (ModelState.IsValid)
            {
                db.dataFindings.Add(tbl_datafindings);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tbl_datafindings);
        }
        
        //
        // GET: /dataFindings/Edit/5
 
        public ActionResult Edit(int id)
        {
            tbl_dataFindings tbl_datafindings = db.dataFindings.Find(id);
            return View(tbl_datafindings);
        }

        //
        // POST: /dataFindings/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_dataFindings tbl_datafindings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_datafindings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_datafindings);
        }

        //
        // GET: /dataFindings/Delete/5
 
        public ActionResult Delete(int id)
        {
            tbl_dataFindings tbl_datafindings = db.dataFindings.Find(id);
            return View(tbl_datafindings);
        }

        //
        // POST: /dataFindings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            tbl_dataFindings tbl_datafindings = db.dataFindings.Find(id);
            db.dataFindings.Remove(tbl_datafindings);
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