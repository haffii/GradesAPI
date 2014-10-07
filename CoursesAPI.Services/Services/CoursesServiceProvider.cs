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

        public ProjectDTO AddProject(ProjectDTO model)
        {

            Project tempP = new Project();
            
            tempP.Name = model.Name;
            tempP.ProjectGroupID = model.ProjectGroupID;
            tempP.CourseInstanceID = model.CourseInstanceID;
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

        public ProjectGroup GetProjectGroup(int ProjectGroupID)
        {
            return _projectgroups.All().SingleOrDefault(p => p.ID == ProjectGroupID);
        }

        public Project GetProject(int ProjectID)
        {
            return _projects.All().SingleOrDefault(p => p.ID == ProjectID);
        }

        public FinalGradeViewModel GetFinalGrade(int CourseInstanceID,string SSN)
        {

            /*var allProjects = (from p in _projects.All()
                                        join pg in _projectgroups.All() on p.ProjectGroupID equals pg.ID
                                        where p.CourseInstanceID == CourseInstanceID
                                        select  p 
                                      ).ToList();*/
            var a = (from pg in _projectgroups.All()
                     join p in _projects.All() on pg.ID equals p.ProjectGroupID
                     where p.CourseInstanceID == CourseInstanceID
                     select pg).ToList();
           
            List<ProjectGroup> mypg = new List<ProjectGroup>();
            Boolean exists =false;
             for (int i = 0; i < a.Count(); i++)
             {
                 if (i == 0)
                 {
                     mypg.Add(a[i]);
                 }
                 else
                 {
                     for(int x = 0; x<mypg.Count(); x++)
                     {
                         if (a[i] == mypg[x])
                         {
                             exists = true;
                             break;
                         }
                     }
                     if(!exists)
                     {
                         mypg.Add(a[i]);
                     }
                     exists = false;
                 }
             }
             double? totalGrade = 0;
             double totalWeight = 0;
             Boolean failed = false;
             for (int i = 0; i < mypg.Count(); i++) {
                 int ID = mypg[i].ID;
             var allMyGrades = (from g in _grades.All()
                                join p in _persons.All() on g.PersonID equals p.SSN
                                where p.SSN == SSN
                                join z in _projects.All() on g.ProjectID equals z.ID
                                join k in _projectgroups.All() on z.ProjectGroupID equals k.ID
                                where k.ID == ID
                                select new CombinedKnowledge
                                {
                                    ProjectId = z.ID,
                                    MinGradeToPassCourse = z.MinGradeToPassCourse,
                                    ProjectName = z.Name,
                                    ProjectGroupId = z.ProjectGroupID,
                                    Weight = z.Weight,
                                    GradeIs = g.GradeIs,
                                    SSN = p.SSN,
                                    GradesProjectCount = k.GradesProjectCount,
                                    ProjectGroupName = k.name

                                }
                      ).ToList();
                  allMyGrades.OrderBy(b => b.GradeIs);
                  List<CombinedKnowledge> GradesThatCount = new List<CombinedKnowledge>();
                  double mul;
                 if(allMyGrades.Count() > allMyGrades[0].GradesProjectCount)
                 {
                     for (int x = 0; x < allMyGrades[0].GradesProjectCount; x++) { 
                         GradesThatCount.Add(allMyGrades.Last());

                         mul = System.Convert.ToDouble(allMyGrades.Last().Weight);
                         mul /= 100;
                         totalGrade += allMyGrades.Last().GradeIs * mul;
                         totalWeight += mul;
                         allMyGrades.Remove(allMyGrades.Last());
                         System.Diagnostics.Debug.WriteLine(totalWeight);
                     }
                    
                 }
                 else
                 {
                     for (int x = 0; x < allMyGrades.Count(); x++)
                     {
                         if (allMyGrades[x].MinGradeToPassCourse != null && allMyGrades[x].MinGradeToPassCourse > allMyGrades[x].GradeIs)
                         {
                             failed = true;
                             break;
                         }
                         mul = System.Convert.ToDouble(allMyGrades[x].Weight);
                         mul /= 100;
                         
                         totalGrade += allMyGrades[x].GradeIs * mul;
                         totalWeight += mul;

                     }

                 }
                 
             }

            //This rounds up/down for FinalGrade
             double Final = Convert.ToDouble(totalGrade) * 2;
             Final = Math.Round(Final, MidpointRounding.AwayFromZero) / 2;
             if (Final > 10) { Final = 10; }
            return new FinalGradeViewModel
            {
                PercentCompleted = totalWeight*100,
                GradeCompleted = totalGrade,
                FinalGrade = Final
                
            };
            
        }

	}
}
