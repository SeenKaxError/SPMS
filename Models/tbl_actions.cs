using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SafetyPatrol.Models
{
    public class tbl_actions
    {
        [Key]
        public int recno { get; set; }

        public string itemcode { get; set; }
        public string actioncode { get; set; }
        public string p_actioncode { get; set; }
        public string actionimage { get; set; }
        public string remarks { get; set; }
        public string empno { get; set; }
        public DateTime recdate { get; set; }


        public tbl_actions() {
             recdate = DateTime.Now;
        }


    }
}