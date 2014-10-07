using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class FinalGradeDTO
    {
        public string PersonID { get; set; }
        public double? FinalGrade { get; set; }
        public double PercentCompleted { get; set; }
        public double? GradeCompleted { get; set; }
        public string PassFail { get; set; }


    }
}
