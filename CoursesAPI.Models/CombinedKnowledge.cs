using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// this is a Model that consist of alot of information from varius tables from a nice query
    /// </summary>
    public class CombinedKnowledge
    {
    public int   ProjectId {get;set;}
    public double? MinGradeToPassCourse {get;set;}
    public string ProjectName{get;set;}
    public int ProjectGroupId {get;set;}
    public int Weight{get;set;}
    public double? GradeIs {get;set;}
    public string SSN {get;set;}
    public int GradesProjectCount {get;set;}
    public string ProjectGroupName {get;set;}
    }
}
