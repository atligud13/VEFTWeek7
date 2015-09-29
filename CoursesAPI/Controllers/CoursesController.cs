using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using System.Collections.Generic;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		[HttpGet]
		[AllowAnonymous]
		public IHttpActionResult GetCoursesBySemester(string semester = null, int page = 1)
		{
            string language = Request.Headers.AcceptLanguage.ToString();
            return Ok(_service.GetCourseInstancesBySemester(semester, page, language));
		}

        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        public IHttpActionResult GetCourseByID(int ID)
        {
            return Ok(_service.GetCourseInstanceByID(ID));
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
		[Route("{id}/teachers")]
		public IHttpActionResult AddTeacher(int id, AddTeacherViewModel model)
		{
			var result = _service.AddTeacherToCourse(id, model);
			return Created("TODO", result);
		}
	}
}
