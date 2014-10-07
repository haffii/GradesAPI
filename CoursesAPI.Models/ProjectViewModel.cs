using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// what we need to add a Project to the projects table
    /// </summary>
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public int ProjectGroupID { get; set; } 
        public int CourseInstanceID { get; set; }
        public int? OnlyHigherThanProjectID { get; set; }
        public int Weight { get; set; }
        public double? MinGradeToPassCourse { get; set; }
    }
}
