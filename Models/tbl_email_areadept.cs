using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity; 
namespace SafetyPatrol.Models
{
    public class tbl_email_areadept
    {
        [Key]
        public int recno { get; set; }
        public string gi_code { get; set; }
        public string empno { get; set; }
        public int ulevel { get; set; }
    }
    public class email_areadept : DbContext
    {
        public email_areadept() : base("FindingsDatabase") {
        }
      
        public DbSet<tbl_email_areadept> ead { get; set; }
         

    }
}