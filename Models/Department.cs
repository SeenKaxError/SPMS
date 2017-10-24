using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace SafetyPatrol.Models
{
    public class tbl_department
    {
        [Key]
        public int recno { get; set; }
        public string department { get; set; }
        public string description { get; set; }

    }
}