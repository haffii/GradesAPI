using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

        [HttpGet]
		[Route("{courseInstanceID:int}/teachers")]
		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
            return _service.GetCourseTeachers(courseInstanceID);
		}
		
        [HttpGet]
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}
        /// <summary>
        /// Adds a ProjectGroup into the table ProjectGroup in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AddProjectGroupDTO</returns>
        [HttpPost]
        [Route("projectgroups")]
        public AddProjectGroupDTO AddProjectGroup(AddProjectGroupDTO model)
        {
           return _service.AddProjectGroup(model);
        }

        /// <summary>
        /// Adds a new Project to the table Projects in the Database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ProjectDTO model</returns>
        [HttpPost]
        [Route("project")]
        public ProjectDTO AddProject(ProjectDTO model)
        {
            return _service.AddProject(model);
        }

        /// <summary>
        /// Adds a new grade to the table grades in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GradeDTO</returns>
        [HttpPost]
        [Route("grade")]
        public GradeDTO AddGrade(GradeDTO model)
        {
            return _service.AddGrade(model);
        }

        /// <summary>
        /// Returns a grade for a given ProjectId and SSN
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns>GradeViewModel</returns>
        [HttpGet]
        [Route("grade/{ProjectID}")]
        public GradeViewModel GetGrade(int ProjectID)
        {
            string SSN = "2211902119";
            return _service.GetGrade(ProjectID,SSN);
        }

        /// <summary>
        /// Calculates a Students FinalGrade
        /// </summary>
        /// <param name="CourseInstanceID"></param>
        /// <returns>FinalGradeViewModel</returns>
       [HttpGet]
       [Route("{CourseInstanceID}/finalgrade")]
        public FinalGradeViewModel FinalGrade(int CourseInstanceID)
       {
           string SSN = "2211902119";
           return _service.GetFinalGrade(CourseInstanceID, SSN);
       }



	}
}
