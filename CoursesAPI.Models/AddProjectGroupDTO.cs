using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    /// <summary>
    /// What we take in to add to the table ProjectGroup
    /// </summary>
    public class AddProjectGroupViewModel
    {
        [Required]
        public string Name { get; set; }
        public int GradesProjectCount { get; set; }
    }
}
