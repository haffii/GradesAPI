using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ProjectGroupID { get; set; }

        public int CourseInstanceID { get; set; }

        public int OnlyHigherThanProjectID { get; set; }

        public int Weight { get; set; }

        public int MinGradeToPassCourse { get; set; }

    }
}
