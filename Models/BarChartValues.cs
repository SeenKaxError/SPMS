using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyPatrol.Models
{
    public class BarChartValue
    {
        public string DeptName { get; set; }
        public decimal ChartValue { get; set; }
        public decimal  completed { get; set; }
        public decimal review { get; set; }
        public int pending { get; set; }

        public BarChartValue() {
            
            DeptName =  "";
            ChartValue = 0;
            completed = 0;
            review = 0;
            pending = 0;
        }
    }

    public class catcount {


        public string categories { get; set; }
        public int count1 { get; set; }

        public catcount() {
          categories = "";
          count1 = 0;
           
        }
    }
    public class dashboard {
        public List<BarChartValue>  bcv { get; set; }
        public List<catcount> bcc { get; set; }

        
        public dashboard() {
             
        }
    }
}