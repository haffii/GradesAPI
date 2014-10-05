using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
<<<<<<< HEAD
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;

=======
>>>>>>> origin/master

namespace CoursesAPI.Tests.Services
{
	[TestClass]
	public class CourseServicesTests
	{
		[TestInitialize]
		public void Setup()
		{
			// TODO: code which will be executed before each test!
		}

		[TestMethod]
<<<<<<< HEAD
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
        public void AddProject()
        {
            // Arrange:
            var projectGroups = new List<ProjectGroup> { };
            _uow.SetRepositoryData(projectGroups);

            AddProjectGroupDTO pg = new AddProjectGroupDTO{
                Name = "TestP",
                GradesProjectCount = 2,
            };
            // Act:
            var result = _service.AddProjectGroup(pg);
            var pgResult = _uow.GetRepository<ProjectGroup>();
            // Assert:
            //Assert.AreEqual(1, );
        }

        [TestMethod]
        public void CoursesGetStudentListInDeregisteredStudents()
        {
            // Arrange:

            // Act:

            // Assert:
        }
=======
		public void TestMethod1()
		{
			// Arrange:

			// Act:

			// Assert:
		}
>>>>>>> origin/master
	}
}
