using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace SafetyPatrol.Models
{
    public class tbl_group
    {
        [Key]
        public int recno { get; set; }
        public string gi_code { get; set; }
        public string groupdetail { get; set; }
    }
}