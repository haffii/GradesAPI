using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    public class AddProjectGroupViewModel
    {
        [Required]
        public string Name { get; set; }
        public int GradesProjectCount { get; set; }
    }
}
