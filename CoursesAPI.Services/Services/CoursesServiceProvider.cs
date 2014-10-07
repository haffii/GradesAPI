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
            
            var result = (from tr in _teacherRegistrations.All()
                          join p in _persons.All() on tr.SSN equals p.SSN
                          where tr.CourseInstanceID == courseInstanceID
                          select p
                          ).ToList();

			return result;
		}

        public AddProjectGroupViewModel AddProjectGroup(AddProjectGroupViewModel model)
        {

            ProjectGroup tempPG = new ProjectGroup();
            tempPG.GradesProjectCount = model.GradesProjectCount;
            tempPG.name = model.Name;
            _projectgroups.Add(tempPG);
            _uow.Save();
            return model;
        }

        public ProjectViewModel AddProject(ProjectViewModel model)
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

        public GradeViewModels AddGrade(GradeViewModels model)
        {

            Grade tempG = new Grade();
            tempG.PersonID = model.PersonID;
            tempG.ProjectID = model.ProjectID;
            tempG.GradeIs = model.GradeIs;

            _grades.Add(tempG);
            _uow.Save();
            return model;
        }

        public GradeDTO GetGrade(int ProjectID, string SSN)
        {

            var tempGrade = _grades.All().SingleOrDefault(g => g.ProjectID == ProjectID && g.PersonID == SSN);
            var tempProject = _projects.All().SingleOrDefault(p => p.ID == ProjectID);

            var pos = GradePos(ProjectID, SSN);
            System.Diagnostics.Debug.WriteLine(SSN);
            return new GradeDTO
            {
                ProjectName = tempProject.Name,
                Grade = tempGrade.GradeIs,
                PositionFrom = pos.From,
                PositionTo = pos.To
                
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

        public FinalGradeDTO GetFinalGrade(int CourseInstanceID,string SSN)
        {
   
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
                  allMyGrades = allMyGrades.OrderBy(b => b.GradeIs).ToList();
                  List<CombinedKnowledge> GradesThatCount = new List<CombinedKnowledge>();
                 double mul;

                 
                 if (!(allMyGrades.Count() == 0)) { 
                 if(allMyGrades.Count() > allMyGrades.First().GradesProjectCount)
                 {
                     for (int x = 0; x < allMyGrades[0].GradesProjectCount; x++) {
                         //GradesThatCount.Add(allMyGrades.Last());

                         if (allMyGrades[x].MinGradeToPassCourse != null && allMyGrades[x].MinGradeToPassCourse > allMyGrades[x].GradeIs)
                         {
                             failed = true;
                            // break;
                         }
                         mul = System.Convert.ToDouble(allMyGrades.Last().Weight);
                         mul /= 100;
                         totalGrade += allMyGrades.Last().GradeIs * mul;
                         totalWeight += mul;
                         
                         allMyGrades.Remove(allMyGrades.Last());
                         
                     }
                    
                 }
                 else
                 {
                     for (int x = 0; x < allMyGrades.Count(); x++)
                     {
                         if (allMyGrades[x].MinGradeToPassCourse != null && allMyGrades[x].MinGradeToPassCourse > allMyGrades[x].GradeIs)
                         {
                             failed = true;
                            // break;
                         }
                         mul = System.Convert.ToDouble(allMyGrades[x].Weight);
                         mul /= 100;
                         
                         totalGrade += allMyGrades[x].GradeIs * mul;
                         totalWeight += mul;

                     }

                 }
                 } 
             }

            //This rounds up/down for FinalGrade
             double Final = Convert.ToDouble(totalGrade) * 2;
             Final = Math.Round(Final, MidpointRounding.AwayFromZero) / 2;
             if (Final > 10) { Final = 10; }


            string passFail ;
            if (failed) { passFail = "Failed"; }
            else{passFail = "Passed";}
             
            var fgvm =  new FinalGradeDTO
            {
                PersonID = SSN,
                PercentCompleted = totalWeight*100,
                GradeCompleted = totalGrade,
                FinalGrade = Final,
                PassFail = passFail

                
            };
            
            return fgvm;
            
        }
    
       public List<FinalGradeDTO> GetAllFinalGrades(int CourseInstanceID){

           List<Person> StudentsInCourse  = (from g in _grades.All() 
                         join p in _projects.All() on g.ProjectID equals p.ID
                         where p.CourseInstanceID == CourseInstanceID
                         join per in _persons.All() on g.PersonID equals per.SSN
                         select per).ToList();
           List<FinalGradeDTO> returnValue = new List<FinalGradeDTO>();
           List<Person> singular = new List<Person>();
           Boolean exists = false;
           foreach(Person p in StudentsInCourse)
           {
               for(int i = 0; i<singular.Count();i++)
               {
                   if(singular[i].SSN == p.SSN)
                   {
                       exists = true;
                       break;
                   }
               }
               if (!exists) { singular.Add(p); }
               exists = false;
           }
           foreach(Person p in singular)
           {            
               returnValue.Add(GetFinalGrade(CourseInstanceID, p.SSN));  
          
           }
           
           return returnValue;
           
       }
       public GradePositionDTO GradePos(int ProjectID, string PersonID)
       {
           var allGrades = (from g in _grades.All() where g.ProjectID == ProjectID select g).ToList();
           var myGrade = _grades.All().SingleOrDefault(g => g.ProjectID == ProjectID && g.PersonID == PersonID);

           allGrades = allGrades.OrderBy(fg => fg.GradeIs).ToList();
           int from = 0;
           int to = 0;
           for (int i = 0; i < allGrades.Count(); i++)
           {
               
               if (allGrades[i].GradeIs == myGrade.GradeIs)
               {
                   if (from == 0)
                   {
                       from = (allGrades.Count) - (i);
                       to = from;

                   }
                   else 
                   {
                       to = (allGrades.Count) - (i);
                       
                       //System.Diagnostics.Debug.WriteLine(allGrades.First().GradeIs);
                   }
               }

           }
           
           return new GradePositionDTO
           {
               From = from,
               To = to
           };
          
       }
       public List<GradeDTO> GetGrades(int ProjectID)
       {
           List<GradeDTO> allGrades = new List<GradeDTO>();
           var all = (from g in _grades.All() where g.ProjectID == ProjectID select g).ToList();
           foreach (var a in all)
           {
               allGrades.Add(GetGrade(ProjectID, a.PersonID));
           }
           return allGrades;
       }
	}
}
