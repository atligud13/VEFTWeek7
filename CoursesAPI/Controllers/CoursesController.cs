using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using System.Collections.Generic;
using WebApi.OutputCache.V2;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/courses")]
    [AutoInvalidateCacheOutput]
    public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		[HttpGet]
		[AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
		public IHttpActionResult GetCoursesBySemester(string semester = null, int page = 1)
		{
            string language = Request.Headers.AcceptLanguage.ToString();
            return Ok(_service.GetCourseInstancesBySemester(semester, page, language));
		}

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCourseByID(int ID)
        {
            return Ok(_service.GetCourseInstanceByID(ID));
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddCourse(AddCourseViewModel model)
		{
			return StatusCode(System.Net.HttpStatusCode.Created);
		}
	}
}
