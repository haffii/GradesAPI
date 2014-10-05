using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    public class AddProjectGroupDTO
    {
        [Required]
        public string Name { get; set; }
        public int GradesProjectCount { get; set; }
    }
}
