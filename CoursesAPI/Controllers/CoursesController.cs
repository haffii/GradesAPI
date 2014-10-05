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
        [Route("teacher/projectgroups")]
        public AddProjectGroupViewModel AddProjectGroup(AddProjectGroupViewModel model)
        {
           return _service.AddProjectGroup(model);
        }

        [HttpPost]
        [Route("teacher/project")]
        public ProjectViewModel AddProject(ProjectViewModel model)
        {
            return _service.AddProject(model);
        }

        [HttpPost]
        [Route("teacher/grade")]
        public GradeViewModel AddGrade(GradeViewModel model)
        {
            return _service.AddGrade(model);
        }

        [HttpGet]
        [Route("{courseInstanceID:int}/student/{ProjectID:int}")]
        public GradeDTO GetGrades(int CourseInstanceID,int ProjectID)
        {
            string SSN = "2211902119";
            return _service.GetGrades(CourseInstanceID,ProjectID,SSN);
        }
	}
}
