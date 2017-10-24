using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace SafetyPatrol.Models
{
    public class FindingsDatabase : DbContext
    {
        public FindingsDatabase() : base("FindingsDatabase")
        {
        }
        
        public DbSet<tbl_dataFindings> dataFindings { get; set; }
        public DbSet<tbl_images> images { get; set; }
        public DbSet<tbl_classification> classification { get; set; }
        public DbSet<tbl_actions> d_actions { get; set; }
        public DbSet<tbl_areadept> areadept { get; set; }
        public DbSet<tbl_email_areadept> emaildept { get; set; }
        public DbSet<tbl_department> dept { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}