using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SafetyPatrol.Models
{
    public class tbl_areadept
    {
        [Key]
        public int recno { get; set; }
        public string department { get; set; }
        public string areaname { get; set; }
        public string gi_code { get; set; }

    }
}