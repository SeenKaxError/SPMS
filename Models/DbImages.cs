using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace SafetyPatrol.Models
{
    public class DbImages : DbContext
    {
        public DbImages()  : base("FindingsDatabase")
        {
        }
        public DbSet<tbl_images> images { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}