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
		
        
        /// <summary>
        /// Adds a ProjectGroup into the table ProjectGroup in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AddProjectGroupDTO</returns>
        [HttpPost]
        [Route("projectgroups")]
        public AddProjectGroupViewModel AddProjectGroup(AddProjectGroupViewModel model)
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
        public ProjectViewModel AddProject(ProjectViewModel model)
        {
            return _service.AddProject(model);
        }

        /// <summary>
        /// Adds a new grade to the table grades in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GradeDTO</returns>
        [HttpPost]
        [Route("teacher/grade")]
        public GradeViewModels AddGrade(GradeViewModels model)
        {
            return _service.AddGrade(model);
        }

        /// <summary>
        /// Returns a grade for a given ProjectId and SSN
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns>GradeViewModel</returns>
        [HttpGet]
        [Route("grade/{ProjectID}/{SSN}")]
        public GradeDTO GetGrade(int ProjectID,string SSN)
        {           
            return _service.GetGrade(ProjectID,SSN);
        }
        /// <summary>
        /// Teacher can Get a list of all grades for a given project
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teacher/grades/{ProjectID}")]
        public List<GradeDTO> GetGrades(int ProjectID)
        {            
            return _service.GetGrades(ProjectID);
        }

        /// <summary>
        /// Calculates a Students FinalGrade
        /// </summary>
        /// <param name="CourseInstanceID"></param>
        /// <returns>FinalGradeViewModel</returns>
       [HttpGet]
       [Route("{CourseInstanceID}/finalgrade/{SSN}")]
        public FinalGradeDTO FinalGrade(int CourseInstanceID,string SSN)
       {
     
           return _service.GetFinalGrade(CourseInstanceID, SSN);
       }

       [HttpGet]
       [Route("{CourseInstanceID}/finalgrades")]
       public List<FinalGradeDTO> AllFinalGrades(int CourseInstanceID)
       {
           
            return _service.GetAllFinalGrades(CourseInstanceID);

       }
      



	}
}
