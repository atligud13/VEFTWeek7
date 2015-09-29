using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Exceptions;
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

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

		/// <summary>
		/// You should implement this function, such that all tests will pass.
		/// </summary>
		/// <param name="courseInstanceID">The ID of the course instance which the teacher will be registered to.</param>
		/// <param name="model">The data which indicates which person should be added as a teacher, and in what role.</param>
		/// <returns>Should return basic information about the person.</returns>
		public PersonDTO AddTeacherToCourse(int courseInstanceID, AddTeacherViewModel model)
		{
			var course = _courseInstances.All().SingleOrDefault(x => x.ID == courseInstanceID);
			if (course == null)
			{
				throw new AppObjectNotFoundException(ErrorCodes.INVALID_COURSEINSTANCEID);
			}

			// TODO: implement this logic!
			return null;
		}

		/// <summary>
		/// You should write tests for this function. You will also need to
		/// modify it, such that it will correctly return the name of the main
		/// teacher of each course.
		/// </summary>
		/// <param name="semester"></param>
		/// <param name="page">1-based index of the requested page.</param>
		/// <returns></returns>
		public List<CourseInstanceDTO> GetCourseInstancesBySemester(string semester = null, int page = 1, string language = "en-GB")
		{
            const int PAGE_SIZE = 10;
            bool languageEN = true;

            if (page < 1) throw new AppValidationException("PAGE_OUT_OF_BOUNDS");

			if (string.IsNullOrEmpty(semester))
			{
				semester = "20153";
			}

            if(language == "is-IS")
            {
                languageEN = false;
            }

            List<CourseInstanceDTO> courses = (from c in _courseInstances.All()
                join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                where c.SemesterID == semester
                orderby ct.CourseID
                select new CourseInstanceDTO
                {
                    Name = languageEN? ct.NameEN : ct.Name,
                    TemplateID = ct.CourseID,
                    CourseInstanceID = c.ID,
                    MainTeacher = ""
                }).Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();

            return courses;
		}

        /// <summary>
        /// Returns a single course with the specified ID
        /// If no course is found then a course not found exception is thrown
        /// </summary>
        /// <param name="courseModel"></param>
        /// <returns></returns>
        public CourseInstanceDTO GetCourseInstanceByID(int ID)
        {
            var course = _courseInstances.All().SingleOrDefault(x => x.ID == ID);
            var courseTemplate = _courseTemplates.All().SingleOrDefault(x => x.CourseID == course.CourseID);

            if (course == null || courseTemplate == null)
            {
                throw new AppObjectNotFoundException();
            }

            var returnValue = new CourseInstanceDTO
            {
                Name = courseTemplate.Name,
                CourseInstanceID = course.ID,
                MainTeacher = "",
                TemplateID = courseTemplate.CourseID
            };

            return returnValue;
        }
	}

    
}
