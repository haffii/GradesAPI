﻿using System;
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

            AddProjectGroupViewModel pg = new AddProjectGroupViewModel{
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

            ProjectViewModel p = new ProjectViewModel
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

        [TestMethod]
        public void GettingGradesAndFinalGrades()
        {
            // Arrange:

            var persons = new List<Person>
            {
                new Person
                {
                    SSN = "2701903249",
                    Name = "Stefan",
                    Email = "dsadsa",

                },
                new Person
                {
                    SSN = "2901903249",
                    Name = "Stefan",
                    Email = "dsadsa",

                },
                new Person
                {
                    SSN = "2801903249",
                    Name = "Stefa",
                    Email = "ddsa",

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

            var grades = new List<Grade> { 
                new Grade
                {
                 ID = 0,
                 ProjectID = 0,
                 PersonID = "2701903249",
                 GradeIs = 5,
                },
                new Grade
                {
                 ID = 1,
                 ProjectID = 0,
                 PersonID = "2801903249",
                 GradeIs = 10,
                },
                new Grade
                {
                 ID = 2,
                 ProjectID = 1,
                 PersonID = "2701903249",
                 GradeIs = 5,
                },
                new Grade
                {
                 ID = 3,
                 ProjectID = 1,
                 PersonID = "2801903249",
                 GradeIs = 5,
                },
                new Grade
                {
                 ID = 4,
                 ProjectID = 0,
                 PersonID = "2901903249",
                 GradeIs = 5,
                }
            };

            var projects = new List<Project>
            {
                new Project{
                    ID = 0,
                    Name = "testP",
                    ProjectGroupID = 0,
                    CourseInstanceID = 1,
                    OnlyHigherThanProjectID = null,
                    Weight = 50,
                    MinGradeToPassCourse = 5,
                },

                new Project{
                    ID = 1,
                    Name = "testP",
                    ProjectGroupID = 0,
                    CourseInstanceID = 1,
                    OnlyHigherThanProjectID = null,
                    Weight = 50,
                    MinGradeToPassCourse = 5,
                },
                new Project{
                    ID = 2,
                    Name = "testP",
                    ProjectGroupID = 0,
                    CourseInstanceID = 1,
                    OnlyHigherThanProjectID = null,
                    Weight = 50,
                    MinGradeToPassCourse = 5,
                },

                new Project{
                    ID = 3,
                    Name = "testP",
                    ProjectGroupID = 0,
                    CourseInstanceID = 1,
                    OnlyHigherThanProjectID = null,
                    Weight = 50,
                    MinGradeToPassCourse = 5,
                }
            };

            var projectGroup = new List<ProjectGroup>
            {
                new ProjectGroup{
                    ID = 0,
                    name = "test group",
                    GradesProjectCount = 2,
                }
            };

            _uow.SetRepositoryData(projectGroup);
            _uow.SetRepositoryData(projects);
            _uow.SetRepositoryData(grades);
            _uow.SetRepositoryData(persons);
            _uow.SetRepositoryData(courseInstances);

            // Act:
            List<FinalGradeDTO> finalGradeList = _service.GetAllFinalGrades(1);
            FinalGradeDTO finalGrade = _service.GetFinalGrade(1, "2701903249");
            List<GradeDTO> gradeList = _service.GetGrades(0);
            GradePositionDTO gradePos = _service.GradePos(0, "2701903249");
            GradePositionDTO gradePoss = _service.GradePos(0, "2801903249");
            GradePositionDTO gradePosss = _service.GradePos(0, "2901903249");


            // Assert:
            Assert.AreEqual(5, finalGradeList[0].FinalGrade, "wrong grade in list of all grades");
            Assert.AreEqual(5, finalGrade.FinalGrade, "wrong grade getting a single grade");
            Assert.AreEqual(7.5, finalGradeList[1].FinalGrade, "wrong grade");
            Assert.AreEqual(5, gradeList[0].Grade, "wrong grade in getGrades student 1");
            Assert.AreEqual(10, gradeList[1].Grade, "wrong grade in getGrades student 2");
            Assert.AreEqual(3, gradePos.From);
            Assert.AreEqual(1, gradePoss.From);
            Assert.AreEqual(3, gradePosss.From);
            Assert.AreEqual(2, gradePos.To);
            Assert.AreEqual(1, gradePoss.To);
            Assert.AreEqual(2, gradePosss.To);
        }
	}
}
