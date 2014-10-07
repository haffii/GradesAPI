using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// this is used when we need to get the Position(what place his grade comes in) of a given project
    /// </summary>
    public class GradePositionDTO
    {
        public int From { get; set; }
        public int To { get; set; }
    }
}
