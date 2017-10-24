using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SafetyPatrol.Models {
    public class DBFindingsImagesActions   {


        public tbl_dataFindings dataFindings { get; set; }
        public  IList<tbl_images>  images { get; set; }
        public  IList<tbl_actions> d_actions { get; set; }

        
    }
}