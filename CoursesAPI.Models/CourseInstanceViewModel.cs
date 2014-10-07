namespace CoursesAPI.Models
{
	/// <summary>
	/// Use this to Add to the courseInstanceTable
	/// </summary>
	public class CourseInstanceViewModel
	{
		public int    CourseInstanceID { get; set; }
		public string CourseID         { get; set; }
		public string Name             { get; set; }

		public string MainTeacher      { get; set; }
	}
}
