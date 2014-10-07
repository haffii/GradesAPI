using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
   public class GradeDTO
    {
       public String ProjectName { get; set; }
       public double? Grade { get; set; }

       public int PositionFrom { get; set; }
       public int PositionTo { get; set; }
    }
}
