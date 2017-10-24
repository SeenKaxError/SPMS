using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace SafetyPatrol.Models
{
    public class tbl_classification
    {
        
        [Key]
        public int classid { get; set; }
        [Display(Name="Title")]
        public string classification { get; set; }
        //public List<tbl_dataFindings> dfindings { get; set; }

    }

    
}