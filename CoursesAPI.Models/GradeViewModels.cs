using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    public class GradeViewModels
    {
        /// <summary>
        /// this is what we need to add a grade to the grades table
        /// </summary>
        [Required]
        public string PersonID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        public double? GradeIs { get; set; }

    }
}
