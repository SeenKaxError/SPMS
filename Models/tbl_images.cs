using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SafetyPatrol.Models
{
    public class tbl_images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recno { get; set; }
        [Required]
        public string itemcode { get; set; }
        public string imagename { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
 
        public tbl_images() { }
        public tbl_images(string itemcode , string imagename, string thumbnail) { 
                this.itemcode = itemcode;
                this.imagename = imagename;
                this.thumbnail = thumbnail;
        }
    }
}