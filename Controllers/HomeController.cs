using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPatrol.Models;
using System.Data;

namespace SafetyPatrol.Controllers
{
    public class HomeController : Controller
    {
        FindingsDatabase db = new FindingsDatabase();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Safety Patrol Management System!";
            ViewBag.pagename = "Dashboard";
            if (HttpContext.Session.Count > 0)
            {
                var islogin = HttpContext.Session["islogin"].ToString();
                if (islogin == "1")
                {
                    ViewBag.currentuser = HttpContext.Session["fname"];
                    ViewBag.Message = "";
                    if (HttpContext.Session["userlevel"].ToString() == "0")
                    {
                        return RedirectToAction("Index","Findings");
                    }
                    else
                    {
                         

                        int y;
                        int month;
                        string smonth;

                        if (HttpContext.Request["year"] != null && HttpContext.Request["year"] != "")
                        {
                             y = int.Parse(HttpContext.Request["year"]);
                         }
                         else{
                             y = 0;
                         }
                        if (HttpContext.Request["month"] != null && HttpContext.Request["month"] != "")
                        {
                            smonth = HttpContext.Request["month"].ToString();
                            if(CheckDate(smonth)){
                            month =Convert.ToDateTime(HttpContext.Request["month"].ToString() +  "01, 1900").Month;
                            }
                            else{
                             ViewBag.Message = "Invalid Month Selection!";
                             month = 0;
                             smonth = "";
                            }

                            

                        }
                        else
                        {
                            month = 0;
                            smonth = "";
                        }
                        if (y == 0 && month > 0) {
                            ViewBag.Message = "Please Select Year!";
                        }

                        List<string> lDept = db.dept.Select(s => s.department).Distinct().ToList();
                        List<BarChartValue> bc = new List<BarChartValue>();
                        List<catcount> c3  = new List<catcount>();
                        dashboard d = new dashboard();


                        if(y == 0 ){

                            ViewBag.syear = ""; //DateTime.Today.Year;
                            ViewBag.smonth = ""; 

                        foreach(string l in lDept){
                            var temp = db.dataFindings.Where(s => s.department == l).Where(s => s.ignore == 0).ToList();

                            var c = db.dataFindings.Where(s => s.status == 3).Where(s => s.department == l).Where(s => s.ignore == 0).ToList();
                            var r = db.dataFindings.Where(s => s.status == 2).Where(s => s.department == l).Where(s => s.ignore == 0).ToList();
                            var p = db.dataFindings.Where(s => s.status == 1).Where(s => s.department == l).Where(s => s.ignore == 0).ToList();
                            
                            
                            BarChartValue x = new BarChartValue();
                            x.ChartValue = temp.Count;
                            x.completed = c.Count;
                            x.review = r.Count;
                            x.pending = p.Count;
                            x.DeptName = l ;

                            bc.Add(x);

                         }

                        }
                        else{
                            ViewBag.syear = y;
                            ViewBag.smonth   = smonth ;
                            if (month > 0)
                            {
                                foreach (string l in lDept)
                                {
                                    var temp = db.dataFindings.Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.recdate.Month == month).Where(s => s.ignore == 0).ToList();
                                    var c = db.dataFindings.Where(s => s.status == 3).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.recdate.Month == month).Where(s => s.ignore == 0).ToList();
                                    var r = db.dataFindings.Where(s => s.status == 2).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.recdate.Month == month).Where(s => s.ignore == 0).ToList();
                                    var p = db.dataFindings.Where(s => s.status == 1).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.recdate.Month == month).Where(s => s.ignore == 0).ToList();
                                    BarChartValue x = new BarChartValue();
                                    x.ChartValue = temp.Count;
                                    x.completed = c.Count;
                                    x.review = r.Count;
                                    x.pending = p.Count;
                                    x.DeptName = l;
                                   bc.Add(x);
                                }
                            }
                            else
                            {
                                foreach (string l in lDept)
                                {
                                    var temp = db.dataFindings.Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.ignore == 0).ToList();
                                    var c = db.dataFindings.Where(s => s.status == 3).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.ignore == 0).ToList();
                                    var r = db.dataFindings.Where(s => s.status == 2).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.ignore == 0).ToList();
                                    var p = db.dataFindings.Where(s => s.status == 1).Where(s => s.department == l).Where(s => s.recdate.Year == y).Where(s => s.ignore == 0).ToList();
                                    BarChartValue x = new BarChartValue();
                                    x.ChartValue = temp.Count;
                                    x.completed = c.Count;
                                    x.review = r.Count;
                                    x.pending = p.Count;
                                    x.DeptName = l;
                                    bc.Add(x);
                                }
                            }
                        
                        }

                       


                        List<tbl_classification> categories = db.classification.ToList();
                        foreach (var cat in categories)
                        {
                            //d.cc.count.Add(db.dataFindings.Where(e => e.classification == cat.classification).Count());
                            int cnt = db.dataFindings.Where(e => e.classification == cat.classification).Count();
                            string c1 = cat.classification.ToString();
                            catcount cat1 = new catcount();
                            cat1.categories = c1;
                            cat1.count1 = cnt;

                            c3.Add(cat1);
                        }


                        d.bcv = bc;

                        d.bcc = c3;

                        ViewBag.noOfFindings = db.dataFindings.Where(m => m.ignore == 0).Count();
                        ViewBag.F_pending = db.dataFindings.Where(m => m.status == 1).Where(m => m.ignore == 0).Count();
                        ViewBag.F_review = db.dataFindings.Where(m => m.status == 2).Where(m => m.ignore == 0).Count();
                        ViewBag.F_completed = db.dataFindings.Where(m => m.status == 3).Where(m => m.ignore == 0).Count();
                       
                        return View(d);
                    }
                }
             
            }
            return RedirectToAction("Index", "Login");
        }

        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date + " 01, 1900");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult getTrends() {

            string datason = "{cols: [{id: 'Department', label: 'Department', type: 'string'},{id: 'Completed', label: 'Completed', type: 'number'},{id: 'Pending', label: 'Completed', type: 'number'},{id: 'Review', label: 'Completed', type: 'number'},],rows: [  {c:[{v: 'a'},{v: 20},{v: 30},{v: 25}]}, {c:[{v: 'a'},{v: 20},{v: 30},{v: 25}]},  {c:[{v: 'a'},{v: 20},{v: 30},{v: 25}]},    {c:[{v: 'a'},{v: 20},{v: 30},{v: 25}]},]  }";
            return Content(datason, "json");

        }
    
    }
}
