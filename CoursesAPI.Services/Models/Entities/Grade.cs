﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
   public class Grade
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public string PersonID { get; set; }

        public double? GradeIs { get; set; }

    }
}
