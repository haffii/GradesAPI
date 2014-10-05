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
        private readonly IRepository<Semester> _semesters;
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

            _semesters = _uow.GetRepository<Semester>();
            _grades = _uow.GetRepository<Grade>();
            _projects = _uow.GetRepository<Project>();
            _projectgroups = _uow.GetRepository<ProjectGroup>();

		}

		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
            //TODO
            var result = (from tr in _teacherRegistrations.All()
                          join p in _persons.All() on tr.SSN equals p.SSN
                          where tr.CourseInstanceID == courseInstanceID
                          select p
                          ).ToList();

			return result;
		}

		public List<CourseInstanceDTO> GetCourseInstancesOnSemester(string semester)
		{
			// TODO:
          /* var result = (from ci in _courseInstances.All()
                          join s in _semesters.All() on ci.SemesterID equals s.ID                  
                          where s.ID == semester  
                          join t in _teacherRegistrations.All() on ci.ID equals t.CourseInstanceID
                          where t.Type == 1
                          join p in _persons.All() on t.SSN equals p.SSN
                          select new CourseInstanceDTO
                          {
                              CourseID = ci.CourseID,
                              CourseInstanceID = ci.ID,
                              Name = s.Name,
                              MainTeacher = p.Name
                          }
                         ).ToList(); */
           // í vinnslu, muna að returna result
           
			return null;
		}

		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
			// TODO
			return null;
		}

        public AddProjectGroupDTO AddProjectGroup(AddProjectGroupDTO model)
        {

            ProjectGroup tempPG = new ProjectGroup();
            tempPG.GradesProjectCount = model.GradesProjectCount;
            tempPG.name = model.Name;
            _projectgroups.Add(tempPG);
            _uow.Save();
            return model;
        }

        public ProjectDTO AddProject(ProjectDTO model,int courseInstanceID)
        {

            Project tempP = new Project();
            
            tempP.Name = model.Name;
            tempP.ProjectGroupID = model.ProjectGroupID;
            tempP.CourseInstanceID = courseInstanceID;
            tempP.OnlyHigherThanProjectID = model.OnlyHigherThanProjectID;
            tempP.Weight = model.Weight;
            tempP.MinGradeToPassCourse =  model.MinGradeToPassCourse;

            _projects.Add(tempP);
            _uow.Save();
            return model;
        }

        public GradeDTO AddGrade(GradeDTO model)
        {

            Grade tempG = new Grade();
            tempG.PersonID = model.PersonID;
            tempG.ProjectID = model.ProjectID;
            tempG.GradeIs = model.GradeIs;

            _grades.Add(tempG);
            _uow.Save();
            return model;
        }

        public GradeViewModel GetGrade(int ProjectID, string SSN)
        {

            var tempGrade = _grades.All().SingleOrDefault(g => g.ProjectID == ProjectID && g.PersonID == SSN);
            var tempProject = _projects.All().SingleOrDefault(p => p.ID == ProjectID);


            return new GradeViewModel
            {
                ProjectName = tempProject.Name,
                Grade = tempGrade.GradeIs
                
            };
        }

        public void GetFinalGrade(int CourseInstanceID,string SSN)
        {

            var allMyGrades = (from g in _grades.All()
                          join p in _persons.All() on g.PersonID equals p.SSN
                          where p.SSN == SSN
                          select g
                       ).ToList();
            var allMyProjects = (from p in _projects.All()
                                 join g in allMyGrades on p.ID equals g.ProjectID                                
                                 select p
                        ).ToList();
        }

	}
}
