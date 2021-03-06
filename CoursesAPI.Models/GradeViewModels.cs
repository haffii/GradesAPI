﻿using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    public class GradeViewModels
    {
        [Required]
        public string PersonID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        public double? GradeIs { get; set; }

    }
}
