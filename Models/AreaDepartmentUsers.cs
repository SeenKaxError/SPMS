using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace SafetyPatrol.Models
{
    public class AreaDepartmentUsers : DbContext
    {


        public AreaDepartmentUsers(): base("FindingsDatabase")
        {       
        
        }
        public DbSet<tbl_areadept> area { get; set; }
        public DbSet<tbl_email_areadept> areadeptuser { get; set; }
        public DbSet<tbl_users> users { get; set; }
        public DbSet<tbl_group> gdept { get; set; }
            
    }
}