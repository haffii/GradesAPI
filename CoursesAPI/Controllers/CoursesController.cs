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
        public AddProjectGroupViewModel AddProjectGroup(AddProjectGroupViewModel model)
        {
           return _service.AddProjectGroup(model);
        }
	}
}
