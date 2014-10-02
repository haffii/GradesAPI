using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;
        //---------------------------------------------
        private readonly IRepository<Grade> _grades;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<ProjectGroup> _projectgroups;


		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
            //TODO
			var result =(from t in _teacherRegistrations.All()
                          join cs in _persons.All() on t.SSN equals cs.SSN
                          where t.CourseInstanceID == courseInstanceID
                         select new Person
                         {
                            ID = cs.ID,
                            SSN = cs.SSN,
                            Name = cs.Name,
                            Email = cs.Email
                       }).ToList(); 
    //Fikt fyrir ofan
			return result;
		}

		public List<CourseInstanceDTO> GetCourseInstancesOnSemester(string semester)
		{
			// TODO:
			return null;
		}

		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
			// TODO
			return null;
		}
	}
}
