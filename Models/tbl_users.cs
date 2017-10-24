using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SafetyPatrol.Models
{
    public class tbl_users
    {
        [Key]
        public int recno { get; set; }
        public string empno { get; set; }
        public string emplname { get; set; }
        public string empfname { get; set; }
        public string empmname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int userlevel { get; set; }
        public string aka { get; set; }
    }

    public class UsersDbContext : DbContext
    {

        public UsersDbContext()
            : base("FindingsDatabase")
        {
        }
      public  DbSet<tbl_users> users { get; set; }
    
    }
}