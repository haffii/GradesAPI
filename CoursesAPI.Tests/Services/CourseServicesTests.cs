using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Models.Entities;


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
		public void TeacherListInCourse()
		{
			// Arrange:
            const int courseId = 1;
            var teachers = new List<Person>
            {
                new Person
                {
                    ID = 1,
                    SSN = "2701903249",
                    Name = "Stefán",
                    Email = "stebbi30@gmail.com"
                }
            };

            _uow.SetRepositoryData(teachers);

            var teacherRegistration = new List<TeacherRegistration> { };
            _uow.SetRepositoryData(teacherRegistration);
			// Act:
            var resault = _service.GetCourseTeachers(courseId);

			// Assert:
            Assert.AreEqual(0, resault);
		}

        [TestMethod]
        public void CoursesGetStudentListInInvalidCourse()
        {
            // Arrange:

            // Act:

            // Assert:
        }

        [TestMethod]
        public void CoursesGetStudentListInDeregisteredStudents()
        {
            // Arrange:

            // Act:

            // Assert:
        }
	}
}
