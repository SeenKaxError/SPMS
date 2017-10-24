using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPatrol.Models;
using System.Data;

namespace SafetyPatrol.Controllers
{
    public class AreaDeptController : Controller
    {
        AreaDepartmentUsers adu = new AreaDepartmentUsers();
        AreaDepartmentUser madu = new AreaDepartmentUser();
        email_areadept dbead = new email_areadept();
        public ActionResult Index()
        {

           ViewBag.Areanames =  new SelectList( adu.area.Select(s => s.areaname).Distinct().ToList());
           ViewBag.groupArea = new SelectList(adu.gdept, "gi_code", "groupdetail");
           ViewBag.users = new SelectList(adu.users, "empno", "aka");

            string sdept ="";
            string sele = adu.area.Select(s => s.areaname).FirstOrDefault();
            string selectedarea = HttpContext.Request["selectedarea"];
            string gicode = HttpContext.Request["groupcode"];
            string groupcode = "";
             
            


            if (selectedarea != "" && selectedarea != null)
            {
            
                sdept = adu.area.Where(s => s.areaname == selectedarea).Select(s => s.department).Single().ToString();
                groupcode = adu.area.Where(s => s.areaname == selectedarea).Select(s => s.gi_code).Single().ToString();
                madu.sDepartment = sdept;
                 
            }
            else {
                if (gicode != "" && gicode != null)
                {
                    ViewBag.groupArea = new SelectList(adu.gdept, "gi_code", "groupdetail", gicode);
                    sdept = adu.area.Where(s => s.gi_code == gicode).Select(s => s.department).First().ToString();
                    groupcode = gicode;
                    madu.sDepartment = sdept;
                   
                }
                else
                {
                    selectedarea = sele;
                    sdept = adu.area.Where(s => s.areaname == selectedarea).Select(s => s.department).Single().ToString();
                    groupcode = adu.area.Where(s => s.areaname == selectedarea).Select(s => s.gi_code).Single().ToString();
                    madu.sDepartment = sdept;
                    
                }
            }
            
           
                ViewBag.departmentSelected = sdept;

                List<string> userids = adu.areadeptuser.Where(g => g.gi_code == groupcode).Select(g => g.empno).ToList();
                madu.lusers = adu.users.Where(s => userids.Contains(s.empno)).ToList();
                return View(madu);
        

        }

        [HttpPost]
        public ActionResult Index(string selectedarea = ""){

            ViewBag.Areanames = new SelectList(adu.area.Select(s => s.areaname).Distinct().ToList());
            ViewBag.groupArea = new SelectList(adu.gdept, "gi_code", "groupdetail");
            ViewBag.users = new SelectList(adu.users, "empno", "aka");

            string sdept = "";

            if (selectedarea != "")
            {
                sdept = adu.area.Where(s => s.gi_code == selectedarea).Select(s => s.department).First().ToString();
            }

            ViewBag.departmentSelected = sdept;
            madu.sDepartment = sdept;
            madu.sArea = selectedarea;

            ////////////////////////////////

            List<string> userids = adu.areadeptuser.Where(g => g.gi_code == selectedarea).Select(g => g.empno).ToList();
            madu.lusers = adu.users.Where(s => userids.Contains(s.empno)).ToList();
            return View(madu);
        }

        public ActionResult DeptArea(string gicode="") {
            ViewBag.groupArea = new SelectList(adu.areadeptuser,"gi_code", "group_detail" ); 
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddIncharge( string gcode , string empid ) {

            try {
                tbl_email_areadept tea = new tbl_email_areadept();
                tea.gi_code = gcode;
                tea.empno = empid;
                tea.ulevel = 0;
                adu.areadeptuser.Add(tea);
                adu.SaveChanges();

                return Content("<div class=\"alert alert-success alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>" + empid + " Successfully Added!</div>", "text/html");
            }
            catch(Exception e){
                
                return Content("<div class=\"alert alert-danger alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>" + e.Message + "</div>", "text/html");

            }

         }

        public ActionResult RemoveInCharge(string gcode, string empno) {

            try
            {
              //remove empno from tbl_aredepartmentmails
                 
                //adu.areadeptuser.Where( s => s.empno == empno).Where(s => s.gi_code == gcode). ;
                IList<tbl_email_areadept> r  = adu.areadeptuser.Where(s => s.empno == empno).Where(s => s.gi_code == gcode).ToList();
                 
                
                foreach (var m in r)
                {
                    if (m.empno == empno && m.gi_code == gcode)
                    {
                       adu.areadeptuser.Remove(m);
                       adu.SaveChanges();
                    }

                }
                return Content("<div class=\"alert alert-danger alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>" + empno + " Successfully Remove from the List!</div>", "text/html");
            }
            catch (Exception e)
            {

                return Content("<div class=\"alert alert-danger alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>" + e.Message + "</div>", "text/html");

            }

        
        
        }

        public ActionResult CreateIncharge(string empno, string empname, string lname, string mname, string aka, string email) {


            return View();
        }


    }
}
