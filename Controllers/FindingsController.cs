using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPatrol.Models;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D; 

namespace SafetyPatrol.Controllers
{
        public class FindingsController : Controller
    {


            FindingsDatabase db = new FindingsDatabase();
            ClassificationDatabase cdb = new ClassificationDatabase();
            DbImages dbi = new DbImages();
            DBFindingsImagesActions dfia = new DBFindingsImagesActions();
            UsersDbContext curuser = new UsersDbContext();
             
            const int pageSize = 10;
            
        [HttpGet]
        public ActionResult Index(int page = 1 , int dstatus = 0, string areadept ="") {
                
            if (HttpContext.Session.Count > 0)
            {
                List<tbl_areadept> ListDepartments = new List<tbl_areadept>();
                ListDepartments = db.areadept.ToList();

                string ulevel = HttpContext.Session["userlevel"].ToString();
                  
                 var pagenumber = (page - 1) * pageSize;
                 IList<tbl_dataFindings> Findings;
                 IList<tbl_dataFindings> TFindings;
                if (dstatus == 0)
                {
                    if (areadept != "")
                    {
                        //Findings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                        Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.areacode == areadept).OrderByDescending(p => p.recno).ToList();
                        TFindings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.areacode == areadept).OrderByDescending(p => p.recno).ToList();
                    }
                    else {
                        //Findings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                        Findings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();
                        TFindings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();
                    
                    }
                }
                else
                {
                    if (areadept != "")
                    {
                        //Findings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                        Findings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).Where(p => p.areacode == areadept).OrderByDescending(p => p.recno).ToList();
                        TFindings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).Where(p => p.areacode == areadept).OrderByDescending(p => p.recno).ToList();
                    }
                    else{
                        Findings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();
                        TFindings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();

                    }
                }
                //Findings = db.dataFindings.Where(p => p.status == dstatus).Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();

                //Findings = from p in db.dataFindings where p.status.Equals(2) orderby p.recno descending select p ;
                
                ViewBag.currentPage = page;
                ViewBag.pageSize = pageSize;
                ViewBag.totalPages = Math.Ceiling((double)TFindings.Count() / pageSize);
                ViewBag.dstatus = dstatus;
                var area = db.areadept.Select(r => r.areaname).Distinct().ToList();

                ViewBag.area = area;
                ViewBag.currentuser = HttpContext.Session["fname"];
               
                return View(Findings);
            }
            else
            {
                ViewBag.Message = "Please Login to Continue";
                return RedirectToAction("Index", "Login");
            }
        
        }

        public ActionResult Search(int page = 1, string sitemcode="" , string sdate ="" , string edate ="", string sdept = "" , string sarea = "" , string sstatus= "0", string areadept= "") {

            IList<tbl_dataFindings> Findings;
            //IList<tbl_dataFindings> TFindings;

            var pagenumber = (page - 1) * pageSize;
            
            ViewBag.dstatus = sstatus;
            var area = db.areadept.Select(r => r.areaname).Distinct().ToList();
            ViewBag.area = area;

            ViewBag.icode = sitemcode;
            ViewBag.areadept = areadept;
            if (HttpContext.Session.Count > 0)
            {
  
                string ulevel = HttpContext.Session["userlevel"].ToString();
                  
                   //if (sstatus == "0")
                   // {

                        //sitemcode 
                        //areadept
                        //sdate
                        ///edate
                        //sstatus
                if (sdate == "") {
                    sdate = "01/01/1900";    
                }
                if (edate == "") {
                    edate = DateTime.Now.ToString();
                }
                DateTime sd = DateTime.Parse(sdate);
                DateTime ed = DateTime.Parse(edate);

                        int s = int.Parse(sstatus);
                        if (sstatus == "0")
                        {
                            Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).Where(p =>p.itemcode == sitemcode || p.areacode == areadept).ToList();
                        }
                        else
                        {
                            Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).Where(p => p.status == s).Where(p => p.itemcode== sitemcode || p.areacode == areadept).ToList();
                        }
                        //TFindings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.status == s).Where(p => p.itemcode == sitemcode || p.areacode == areadept).ToList(); 
                        
                    //    if (sdate != "" && sdate != null && sitemcode != "" && sitemcode != null && areadept != "")
                    //    {
                    //        DateTime sd = DateTime.Parse(sdate);
                    //        DateTime ed = DateTime.Parse(edate);

                    //        Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).Where(p => p.itemcode.Contains(sitemcode)).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                    //        TFindings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).Where(p => p.itemcode.Contains(sitemcode)).OrderByDescending(p => p.recno).ToList();
                            
                    //    }
                    //    else if (sitemcode != "" && sitemcode != null   )  
                    //    {

                    //        Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.itemcode.Contains(sitemcode)).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                    //        TFindings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.itemcode.Contains(sitemcode)).ToList();

                    //    }
                    //    else if (sdate != "" && sdate != null)
                    //    {
                    //        DateTime sd = DateTime.Parse(sdate);
                    //        DateTime ed = DateTime.Parse(edate);

                    //        Findings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).OrderByDescending(p => p.recno).Skip(pagenumber).Take(pageSize).ToList();
                    //        TFindings = db.dataFindings.Where(p => p.ignore == 0).Where(p => p.recdate >= sd).Where(p => p.recdate <= ed).OrderByDescending(p => p.recno).ToList();

                    //    }

                    //    else
                    //    {
                         
                    //        Findings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();
                    //        TFindings = db.dataFindings.Where(p => p.ignore == 0).OrderByDescending(p => p.recno).ToList();

                    //        Findings.Where(p => p.ignore == 0).ToList();
                    //    }
                    //}
                    //else
                    //{
                    //    int s = int.Parse(sstatus);
                    //    Findings = db.dataFindings.Where(p => p.status == s ).Where(p => p.ignore == 0).Where(p => p.itemcode.Contains(sitemcode)).OrderByDescending(p => p.recno).ToList();
                    //    TFindings = db.dataFindings.Where(p => p.status == s).Where(p => p.ignore == 0).Where(p => p.itemcode.Contains(sitemcode)).OrderByDescending(p => p.recno).ToList();

                    //}
                    
                    //Findings = from p in db.dataFindings where p.status.Equals(2) orderby p.recno descending select p ;
                     ViewBag.currentPage = page;
                     ViewBag.pageSize = pageSize;
                     ViewBag.totalPages = Math.Ceiling((double) Findings.Count / pageSize);
                     ViewBag.toatalItems = Findings.Count;

                    ViewBag.currentuser = HttpContext.Session["fname"];
               
                return View(Findings);
            }
            else {
                ViewBag.Message = "Please Login to Continue";
                return RedirectToAction("Index", "Login");
            }
        }
    
        public ViewResult Details(string id)
        {
            var dbfia = new DBFindingsImagesActions();
            dbfia.dataFindings =  db.dataFindings.Where(p => p.itemcode == id).SingleOrDefault();
            dbfia.d_actions = db.d_actions.Where(p => p.itemcode == id).ToList();
            dbfia.images = db.images.Where(p => p.itemcode == id).ToList();
         
            return View(dbfia);
        }

        public ActionResult Edit(string id, string m ="")
        {
            
            var classification = new SelectList(db.classification.Select(r => r.classification).Distinct().ToList());
            ViewBag.classification = classification;
            var dbfia = new DBFindingsImagesActions();
            dbfia.dataFindings = db.dataFindings.Where(p => p.itemcode == id).SingleOrDefault();
            dbfia.images = db.images.Where(p => p.itemcode == id).ToList();

            var area = new SelectList(db.areadept.Select(r => r.areaname).Distinct().ToList(), dbfia.dataFindings.areacode);
            ViewBag.areanames = area;
            if (m != "")
            {
                ViewBag.Message = "Datafinding Updated!";
            }
            else {
                ViewBag.Message = "";
            }
            
            return View(dbfia);
        }

        
        [HttpPost]
        public ActionResult Edit(DBFindingsImagesActions  DBFindingsImagesActions, string checkimagepath, string areacode )
        {
            if (ModelState.IsValid)
            {
                DBFindingsImagesActions.dataFindings.imagePath = checkimagepath;
                
                var department = db.areadept.Where(r => r.areaname ==  areacode).Single();
                DBFindingsImagesActions.dataFindings.areacode = areacode;
                DBFindingsImagesActions.dataFindings.areaincharge = department.gi_code;
                DBFindingsImagesActions.dataFindings.department = department.department;

               db.Entry(DBFindingsImagesActions.dataFindings).State = EntityState.Modified;
               db.SaveChanges();
               // ViewBag.mesage = "Data Findings Updated";
               
                return RedirectToAction("Edit", new { id = DBFindingsImagesActions.dataFindings.itemcode , m = "updated" });
            }
            ViewBag.Message("Somethings Wrong! " + DBFindingsImagesActions.dataFindings.itemcode);
            return View(DBFindingsImagesActions);
        }

        public ActionResult NewFindings()
        {

            var classification = new SelectList(db.classification.Select(r => r.classification ).Distinct().ToList());
             //var  classification = new SelectList(db.classification, "classid", "classification");
            ViewBag.classification = classification;

            var area = new SelectList(db.areadept.Select(r => r.areaname).Distinct().ToList());
            //var area = new SelectList(db.areadept, "gi_code", "areaname");
            ViewBag.areanames = area;

             return View();
        }
     
        
        [HttpPost]
        public ActionResult NewFindings(tbl_dataFindings tbl_datafindings, IEnumerable<HttpPostedFileBase> files )
        {
            if (ModelState.IsValid)
            {
                 
                tbl_images im = new tbl_images();
                string month = DateTime.Now.ToString("MM");
                string year = String.Format("{0:X}", System.Convert.ToInt32(DateTime.Now.ToString("yy")));
                string day = DateTime.Now.ToString("dd");
                string reccount = db.dataFindings.Count().ToString();
                if (System.Convert.ToInt32(reccount) < 10)
                {
                    reccount = "00" + reccount;
                }
                else if (System.Convert.ToInt32(reccount) < 100)
                {
                    reccount = "0" + reccount;
                }
                else if (System.Convert.ToInt32(reccount) >= 1000)
                {
                    reccount = "0" + reccount;
                }
                reccount = reccount.Substring(reccount.Length - 3, 3);
                string ditemcode = year + month + day + reccount;
                tbl_datafindings.itemcode = ditemcode;
                if (files  != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            string path = System.IO.Path.Combine(Server.MapPath("~/fileImages"), System.IO.Path.GetFileName(file.FileName));
                            path = Server.MapPath("~/fileImages/" + ditemcode);

                            if (!Directory.Exists(path)) {
                                DirectoryInfo di = Directory.CreateDirectory(path);
                                DirectoryInfo dthumb = Directory.CreateDirectory(path + "/thumb/"); 
   
                            }
                            
                            
                            string ext = System.IO.Path.GetExtension(file.FileName);
                            string newfilename = ditemcode;
                            string tosave =path + "/" + newfilename + ext;
                            int i = 0;
                            
                            
                            while(System.IO.File.Exists(tosave)){
                                i++;
                                newfilename = ditemcode + "_" + i;
                                tosave = path + "/" + newfilename  + ext;

                            }
                            file.SaveAs(tosave);
                            string thumbdir;
                            thumbdir = path + "/thumb/thumb_" + newfilename + ext; 
                            GenerateThumbnails(0.5, file.InputStream, thumbdir);
                            tbl_datafindings.imagePath = ditemcode + "/" + newfilename + ext;
                            im = new tbl_images(ditemcode, ditemcode + "/" + newfilename + ext, ditemcode + "/" + newfilename + ext);
                            dbi.images.Add(im);
                            dbi.SaveChanges();
                          
                        }
                        
                    }

                }

                
                var department = db.areadept.Where(r => r.areaname == tbl_datafindings.areacode).Single();
                
                tbl_datafindings.areaincharge = department.gi_code;
                tbl_datafindings.department = department.department;

                db.dataFindings.Add(tbl_datafindings);
                db.SaveChanges();
                //email Incharge


                // get gi_code || areaincharge
                string gicode = department.gi_code;

                //get list of incharge

                var loi = db.emaildept.Where(s => s.gi_code == gicode).ToList();
                string[] eset;
                eset = new string[loi.Count];
                if (loi.Count > 0)
                {

                    int x = 0;
                    foreach (var emp in loi)
                    {
                        try
                        {

                            string email = curuser.users.Single(p => p.empno == emp.empno).email.ToString();
                            eset[x] = email;
                            x++;
                        }
                        catch (Exception e) { Response.Write(e.Message); }
                    }
                }

                // get_emails
                
                string mailmsg = "<strong>A new finding was found in your Area</strong>";
                mailmsg = mailmsg + "<p>Please Click on the Link below  to  respond with the Finding</p>";

                if (eset.Length > 0)
                {

                    SendEmail(eset, ditemcode, mailmsg , "newFindings");

                }

                ViewBag.Message = "Findings added successfully!";
                return RedirectToAction("Index");

            }
            else
            {
                
                var classification = new SelectList(db.classification.Select(r => r.classification).Distinct().ToList());
                ViewBag.classification = classification;
                var area = new SelectList(db.areadept.Select(r => r.areaname).Distinct().ToList());
                ViewBag.areanames = area;

                ViewBag.Message = "Please Complete required fields";
                return View(tbl_datafindings);
            }
        }

        private void GenerateThumbnails(double scaleFactor, Stream sourcePath,  string targetPath)
        {
             
            using (var image = Image.FromStream(sourcePath))
            {
                // can given width of image as we want
                var newWidth = (int)(image.Width * scaleFactor);
                // can given height of image as we want
                var newHeight = (int)(image.Height * scaleFactor);
                
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                 thumbnailImg.Save(targetPath, image.RawFormat);
                //thumbnailImg.Save(targetPath);
                //thumbnailImg.Save("C:\\test.jpg");
            } 
        }

        public ActionResult AddActions()
        {
            
            return RedirectToAction("Details");

        }
        private string get_acode() {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string[] m = {"A","B","C", "D", "F","G","H", "I", "J", "K","L", "M", "N"};
            return m[DateTime.Now.Month] +  DateTime.Now.Day.ToString("D2") + rand.Next(100,999) + Math.Abs(DateTime.Now.Second - DateTime.Now.Hour).ToString("D2");
        }

        [HttpPost]
        public ActionResult AddActions(string empno, string actionitemcode,HttpPostedFileBase imagefile, string remarks = "", string requestforreview =""  )
        {
            string actioncode = get_acode();
            string actionimage = "";
            //////////////////////
            if (imagefile != null)
            {
                if (imagefile.ContentLength > 0)
                {
                    try
                    {
                        tbl_images im = new tbl_images();
                        string path = System.IO.Path.Combine(Server.MapPath("~/fileImages"), System.IO.Path.GetFileName(imagefile.FileName));
                        path = Server.MapPath("~/fileImages/" + actionitemcode + "/" + actioncode);

                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path);
                        }

                        //var im = new tbl_images();
                        //db.images.Add(im);
                        string ext = System.IO.Path.GetExtension(imagefile.FileName);
                        string newfilename = actioncode;
                        string tosave = path + "/" + newfilename + ext;
                        int i = 0;
                        while (System.IO.File.Exists(tosave))
                        {
                            i++;
                            newfilename = actioncode + "_" + i;
                            tosave = path + "/" + newfilename + ext;

                        }
                        actionimage = actionitemcode + "/" + actioncode + "/" + newfilename + ext;
                        imagefile.SaveAs(tosave);
                        im = new tbl_images(newfilename, actionimage, actionimage);
                        dbi.images.Add(im);
                        dbi.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            }
            ////////////////
            
            
            
            var ac = new tbl_actions() ;
            ac.remarks= remarks;
            ac.itemcode = actionitemcode;
            ac.actioncode = actioncode;
            ac.actionimage = actionimage;
            ac.p_actioncode = "00000";

            ac.empno = empno;
            db.d_actions.Add(ac);
            db.SaveChanges();
             
            //check if request for review
            if (requestforreview == "forreview")
            {
                tbl_dataFindings d_item = db.dataFindings.Single(p => p.itemcode == actionitemcode);
                d_item.status = 2;
                db.SaveChanges();
            }
            // get gi_code || areaincharge
            string gicode = db.dataFindings.Where(s => s.itemcode == actionitemcode).Select(s => s.areaincharge).Single();
            
            //get list of incharge

            var loi = db.emaildept.Where(s => s.gi_code == gicode).ToList();
            string[] eset;
            eset = new string[loi.Count];
            if (loi.Count > 0) {
               
                int x = 0;
                foreach (var emp in loi) {
                    try {
                        
                        string email = curuser.users.Single(p => p.empno == emp.empno).email.ToString();
                        eset[x] =email;
                        x++;
                        }
                    catch (Exception e) { Response.Write(e.Message); } 
                    }
            } 
              
            // get_emails
            if (eset.Length > 0)
            {
                 
                     SendEmail(eset, actionitemcode, remarks);
                     
            }
            return RedirectToAction("Details", new { @id = actionitemcode });

        }


        public ActionResult MarkAsCompleted(string id) {
            try
            {
               var dbfinding = db.dataFindings.Where(m => m.itemcode == id).Single();
                dbfinding.status = 3;
                dbfinding.completeddate = DateTime.Now.Date;
                db.Entry(dbfinding).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception e) {
                 ViewBag.Message = e.Message;
            }

            return RedirectToAction("Details", new { @id = id });

        }
        [HttpPost]
        public ActionResult NotifyIncharge(string itemcode, string message)
        {

            try {
            // get gi_code || areaincharge
            string gicode = db.dataFindings.Where(s => s.itemcode == itemcode).Select(s => s.areaincharge).Single();

            //get list of incharge

            var loi = db.emaildept.Where(s => s.gi_code == gicode).ToList();
            string[] eset;
            eset = new string[loi.Count];
            if (loi.Count > 0)
            {

                int x = 0;
                foreach (var emp in loi)
                {
                    try
                    {

                        string email = curuser.users.Single(p => p.empno == emp.empno).email.ToString();
                        eset[x] = email;
                        x++;
                    }
                    catch (Exception e) { Response.Write(e.Message); }
                }
            }

            // get_emails
            if (eset.Length > 0)
            {

                SendEmail(eset, itemcode, message ,"notifyincharge");
                ViewBag.message = "Email Notification Sent!";
            }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
            }

            return RedirectToAction("Details", new { @id = itemcode });
        
        }

        //Send Email

        public void SendEmail(string[] address, string subject, string message , string state = "")
        {
         
  
          
         System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
         
          msg.IsBodyHtml = true;

          foreach (string email in address)
          {

              if (email != null)
              {
                  msg.To.Add(email); //recipient
              }
               
          }

          msg.IsBodyHtml = true;
          msg.Priority = MailPriority.High;
          msg.Subject = "SES Finding Itemcode : " + subject;

          
          msg.From = new System.Net.Mail.MailAddress("noreply@safetypatrol.kao-phil.com"); //from email
        
          msg.CC.Add("egpaitan@kao-phil.com");
          msg.CC.Add("msebora@kao-phil.com");

          msg.Bcc.Add("mcdagus@kao-phil.com");
             
          
              message = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
              message = message + "<html xmlns='http://www.w3.org/1999/xhtml'>";
              message = message + "<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
              message = message + "<title>Safety Patrol Monitoring System</title>";
              message = message + "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>";
              message = message + "</head>";
              message = message + "<body><div>";
              message = message + "<table border=\"1\" bordercolor=\"black\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">";
              message = message + "<tr><td align=\"center\" bgcolor=\"#003300\" style=\"padding: 20px 0 20px 0; font-weight:bold;color:#ffffff;font-family: \"Helvetica\", \"Arial\", \"Arial Black\", \"Impact\";\"><img src=\"cid:kaologo\" width=\"300px;\"hspace=0  align=baseline border=0 /><h2>SAFETY PATROL MONITORING SYSTEM</h2></td></tr>";
              if (state == "newFindings")
              {
                  message = message + "<tr>";
                  message = message + "<td width=\"600\" bgcolor=\"#ffffff\" valign=\"middle\" style=\"padding: 40px 20px 30px 20px;color:#000000;\">";
                  message = message + "<p style=\"text-align:right;\">" + @DateTime.Now.ToString("d") + "</p>";
                  message = message + "<p>Greetings!</p>";
                  message = message + "<p>A New Finding was found in your Area.</p>";
                  message = message + "<a href='http://" + HttpContext.Request.Url.Host + "/SPMS/Findings/Details/" + subject + "'>Click here to see about the SES findings</a>";
                  message = message + "</td>";
                  message = message + "</tr>";
              }
              else if (state == "notifyincharge")
              {
                  message = message + "<tr>";
                  message = message + "<td width=\"600\" bgcolor=\"#ffffff\" valign=\"middle\" style=\"padding: 40px 20px 30px 20px;color:#000000;\">";
                  message = message + "<p style=\"text-align:right;\">" + @DateTime.Now.ToString("d") + "</p>";
                  message = message + "<p>Greetings!</p>";
                  message = message + "<p>" + message + "</p>";
                  message = message + "<a href='http://" + HttpContext.Request.Url.Host + "/SPMS/Findings/Details/" + subject + "'>Click here to see about the SES findings</a>";
                  message = message + "</td>";
                  message = message + "</tr>";
                 
              }
              else
              {
                  message = message + "<tr>";
                  message = message + "<td width=\"600\" bgcolor=\"#ffffff\" valign=\"middle\" style=\"padding: 40px 20px 30px 20px;color:#000000;\">";
                  message = message + "<p style=\"text-align:right;\">" + @DateTime.Now.ToString("d") + "</p>";
                  message = message + "<p>Greetings!</p>";
                  message = message + "<p>There was an update to your assigned area.</p>";
                  message = message + "<a href='http://" + HttpContext.Request.Url.Host + "/SPMS/Findings/Details/" + subject + "'>Click here to see about the SES findings</a>";
                  message = message + "</td>";
                  message = message + "</tr>";
              }
                  message = message + "<tr><td bgcolor=\"#000000\" style=\"padding: 10px 20px 10px 20px;color:#ffffff;\"> <small>Note:  This email message was auto-generated. Please <span style=\"color: #B0190B;font-weight: bolder;\">DO NOT REPLY</span></small></td></tr>";
                  message = message + "</table>";
                  message = message + "</div></body>";
                  message = message + "</html>";
              
              AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message, null, "text/html");
              LinkedResource imagelink = new LinkedResource(Server.MapPath("/SPMS/Content/images/") + @"\kao_logo.png", "image/png");
              imagelink.ContentId = "kaologo";
              imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
              htmlView.LinkedResources.Add(imagelink);
              msg.AlternateViews.Add(htmlView);

          // you need an smtp server address to send emails
          //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("sa9fi011.kao-phil.com");
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mailhub1.sdd.kao.co.jp"); 

          try
          {
              smtp.Send(msg);
          }
          catch (Exception e) {
              Response.Write(e.Message);
          }

          //return Content("Email Sent!");

 
        }

         // Manage Photos per Findings

        public ActionResult ManagePhoto(string id) {

            IList<tbl_images> imobj = db.images.Where(m => m.itemcode == id).ToList();
            ViewBag.item_code = id;
            return View(imobj);
        }
        
        public ActionResult DeletePhoto(string itemcode)
        {
            return RedirectToAction("ManagePhoto", new { @id = itemcode });
        }

        [HttpPost]
        public ActionResult DeletePhoto(string itemcode, int imgid)
        {
            IList<tbl_images> im = dbi.images.Where(m => m.itemcode == itemcode).ToList();
            foreach (var m in im)
            {
                if (m.recno == imgid)
                {
                    dbi.images.Remove(m);
                    dbi.SaveChanges();
                    break;
                }

            }


            return RedirectToAction("ManagePhoto", new { @id = itemcode });
        }
       [HttpGet]
        public ActionResult AddPhoto(string itemcode) {
            return RedirectToAction("ManagePhoto", new { @id = itemcode });
        }

        [HttpPost]
        public ActionResult AddPhoto(string itemcode, HttpPostedFileBase files) {

            ViewBag.Message = "";
            if(files !=  null){
                tbl_images im = new tbl_images();
                string path = System.IO.Path.Combine(Server.MapPath("~/fileImages"), System.IO.Path.GetFileName(files.FileName));
                path = Server.MapPath("~/fileImages/" + itemcode);

                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                //var im = new tbl_images();
                //db.images.Add(im);
                string ext = System.IO.Path.GetExtension(files.FileName);
                string newfilename = itemcode;
                string tosave = path + "/" + newfilename + ext;
                int i = 0;
                while (System.IO.File.Exists(tosave))
                {
                    i++;
                    newfilename = itemcode + "_" + i;
                    tosave = path + "/" + newfilename + ext;

                }
                files.SaveAs(tosave); 
                im = new tbl_images(itemcode, itemcode + "/" + newfilename + ext, itemcode + "/" + newfilename + ext);
                dbi.images.Add(im);
                dbi.SaveChanges();
            }

            return RedirectToAction("ManagePhoto", new {@id = itemcode});
        }
        
       

        }
}
