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

        [HttpPost]
        [Route("projectgroups")]
        public AddProjectGroupDTO AddProjectGroup(AddProjectGroupDTO model)
        {
           return _service.AddProjectGroup(model);
        }

        [HttpPost]
        [Route("{courseInstanceID}/project")]
        public ProjectDTO AddProject(ProjectDTO model,int courseInstanceID)
        {
            return _service.AddProject(model,courseInstanceID);
        }

        [HttpPost]
        [Route("grade")]
        public GradeDTO AddGrade(GradeDTO model)
        {
            return _service.AddGrade(model);
        }

        [HttpGet]
        [Route("grade/{ProjectID}")]
        public GradeViewModel GetGrade(int ProjectID)
        {
            string SSN = "2211902119";
            return _service.GetGrade(ProjectID,SSN);
        }

       [HttpGet]
       [Route("{CourseInstanceID}/finalgrade")]
        public FinalGradeViewModel FinalGrade(int CourseInstanceID)
       {
           string SSN = "2211902119";
           return _service.GetFinalGrade(CourseInstanceID, SSN);
       }



	}
}
