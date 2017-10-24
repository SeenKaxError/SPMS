using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SafetyPatrol.Models
{
    public class AreaDepartmentUser
    {

        public string   sArea { get; set; }
        public string   sDepartment { get; set; }
         
        public IList<tbl_users> lusers { get; set; }

        
    }
}