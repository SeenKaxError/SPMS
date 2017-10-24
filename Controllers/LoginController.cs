using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPatrol.Models;
using System.Data;

namespace SafetyPatrol.Controllers
{
    public class LoginController : Controller
    {
        UsersDbContext curuser = new UsersDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authen()
        {
            ViewBag.Message = "";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string username, string password) {

                var d_user = curuser.users.Where(u => u.empno == username).Where(u => u.password == password).ToList();

                if (d_user.Count() > 0)
                {
                    // Record Sessions
                    HttpContext.Session.Timeout = 30;
                    HttpContext.Session["islogin"] = "1";
                    HttpContext.Session["username"] = d_user[0].empno;
                    HttpContext.Session["fname"] = d_user[0].empfname;
                    HttpContext.Session["userlevel"] = d_user[0].userlevel;
                    return RedirectToAction("Index", "Home"); //Redirect OR Show home page
                }
                else
                {

                    ViewBag.Message = "Error! Invalid Login Details";
                    return View();
                }
           
        }

        [HttpPost]
        public ActionResult LoginRedirect(string username, string password , string redirec ="") {

            var d_user = curuser.users.Where(u => u.empno == username).Where(u => u.password == password).ToList();

            if (d_user.Count() > 0)
            {
                // Record Sessions
                HttpContext.Session.Timeout = 30;
                HttpContext.Session["islogin"] = "1";
                HttpContext.Session["username"] = d_user[0].empno;
                HttpContext.Session["fname"] = d_user[0].empfname;
                HttpContext.Session["userlevel"] = d_user[0].userlevel;
                if(redirec !=""){
                return Redirect(redirec); //Redirect OR Show home page
                }else{
                return RedirectToAction("Index", "Home"); //Redirect OR Show home page
                }

            }
            else
            {

                ViewBag.Message = "Error! Invalid Login Details";
                ViewBag.message = "Error! Invalid Login Details";

                return Redirect(redirec); //Redirect OR Show home page
            }
           
        }

        public ActionResult Logout()
        {

            HttpContext.Session.RemoveAll();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            
            return RedirectToAction("Index", "Login");
        }

    }
}
