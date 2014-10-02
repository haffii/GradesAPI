using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
    public class ProjectGroup
    {
        public int ID { get; set; }

        public string name { get; set; }

        public int GradesProjectCount { get; set; }
    }
}
