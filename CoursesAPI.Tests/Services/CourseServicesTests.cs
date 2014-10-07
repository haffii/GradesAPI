using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;


namespace CoursesAPI.Tests.Services
{
	[TestClass]
	public class CourseServicesTests
	{
        private CoursesServiceProvider _service;
        private MockUnitOfWork<MockDataContext> _uow;

		[TestInitialize]
		public void Setup()
		{
            _uow = new MockUnitOfWork<MockDataContext>();
            _service = new CoursesServiceProvider(_uow);
			// TODO: code which will be executed before each test!
		}

		[TestMethod]
		public void CheckGrade()
		{
			// Arrange:
            const int courseId = 1;
            const String ssn = "2701903249";

            var persons = new List<Person>
            {
                new Person
                {
                    SSN = "2701903249",
                    Name = "Stefan",
                    Email = "dsadsa",

                }
            };

            var teachers = new List<TeacherRegistration>
            {
                new TeacherRegistration
                {
                    SSN = "2701903249",
                    CourseInstanceID = 1,
                    Type = 1,

                }
            };

            var courseInstances = new List<CourseInstance>
            {
                new CourseInstance
                {
                    ID = 1,
                    CourseID = "dsddsa",
                    SemesterID = "dsa",
                }
            };

            _uow.SetRepositoryData(teachers);
            _uow.SetRepositoryData(persons);
            _uow.SetRepositoryData(courseInstances);

			// Act:
            var list = _service.GetCourseTeachers(courseId);
            Person result = list[0];
            
			// Assert:
            Assert.AreEqual(ssn, result.SSN);
		}

        [TestMethod]
        public void AddProjectGroup()
        {
            // Arrange:
            const String n = "TestP";
            const int c = 2;

            var projectGroups = new List<ProjectGroup> { };
            _uow.SetRepositoryData(projectGroups);

            AddProjectGroupDTO pg = new AddProjectGroupDTO{
                Name = n,
                GradesProjectCount = c,
            };
            // Act:
            var result = _service.AddProjectGroup(pg);

            // Assert:
            var tmp = _service.GetProjectGroup(0);
            Assert.IsNotNull(tmp, "No Project Group Added");
            Assert.AreEqual(n , tmp.name, "Wrong Name");
            Assert.AreEqual(c, tmp.GradesProjectCount, "Wrong Grades Project Count");
        }

        [TestMethod]
        public void AddProject()
        {
            // Arrange:
            var projects = new List<Project> { };
            _uow.SetRepositoryData(projects);

            ProjectDTO p = new ProjectDTO
            {
                Name = "test1",
                ProjectGroupID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 10,
                MinGradeToPassCourse = 5,
                CourseInstanceID = 1
            };

            // Act:
            var a = _service.AddProject(p);

            // Assert:
            var tmp = _service.GetProject(0);
            Assert.IsNotNull(tmp, "No Project Added");
            Assert.AreEqual("test1", tmp.Name);
        }

        [TestMethod]
        public void GetGrade()
        {
            // Arrange:
            var grades = new List<Grade> { 
                new Grade
                {
                 ID = 0,
                 ProjectID = 0,
                 PersonID = "2701903249",
                 GradeIs = 9,
                }
            };

            var projects = new List<Project>
            {
                new Project{
                    ID = 0,
                    Name = "testP",
                    ProjectGroupID = 1,
                    CourseInstanceID = 1,
                    OnlyHigherThanProjectID = null,
                    Weight = 10,
                    MinGradeToPassCourse = 5,
                }
            };

            _uow.SetRepositoryData(grades);
            _uow.SetRepositoryData(projects);

            // Act:
            var a = _service.GetGrade(0,"2701903249");

            // Assert:
            Assert.IsNotNull(a, "No Grade Found");
            Assert.AreEqual(9, a.Grade);
        }
	}
}
