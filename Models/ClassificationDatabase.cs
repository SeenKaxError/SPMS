using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace SafetyPatrol.Models
{
    public class ClassificationDatabase : DbContext
    {

        public ClassificationDatabase() : base("FindingsDatabase")
        {       
        
        }

        public DbSet<tbl_dataFindings> dataFindings { get; set; }
        public DbSet<tbl_classification> Classification { get; set; }
    }
}