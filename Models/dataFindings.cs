using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SafetyPatrol.Models
{
    public class tbl_dataFindings
    {

         [Key]
        public int recno { get; set; }

      
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="Item Code")]
        public string itemcode { get; set; }
        public  string imagePath { get; set; }
        
        [Required(ErrorMessage="Description Required.")]
        [Display(Name="Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Classification Required.")]
        [Display(Name = "Classification")]
        public string classification { get; set; }
        //public IEnumerable<tbl_classification> classification;
        
        [Required(ErrorMessage="AreaCode Required")] 
        [Display(Name = "Area / Location")]
        public string areacode { get; set; }
        public string areaincharge { get; set; }
        [Display(Name="Area Details")]
        public string areadetails { get; set; }
        public string department { get; set; }
        [Display(Name="Status")]
        public byte status { get; set; }
        public string recby { get; set; }
        public DateTime? completeddate { get; set; }
        public DateTime recdate { get; set; }
        public byte ignore { get; set; }

        //public virtual ICollection<tbl_images> Images { get; set; }
        //public virtual ICollection<tbl_actions> actions { get; set; }
       
        public tbl_dataFindings() {
            //tbl_images assignimage = new tbl_images();
            recdate = DateTime.Now;
            status = 1;
            ignore = 0;
        }
 
    }
}