using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Schedulizer_WebApp.Models;
using System.Net;
using AutoMapper;
using Schedulizer_WebApp.ViewModels;

namespace Schedulizer_WebApp.Controllers.Api
{
    [Route("api/schedules/{scheduleName}/courses")]
    public class CourseController : Controller
    {
        private ICourseRepository _repository;

        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get(string scheduleName)
        {
            try
            {
                var results = _repository.GetScheduleByName(scheduleName);

                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<CourseViewModel>>(results.Courses.OrderBy(s => s.CourseCode)));
            }
            catch (Exception ex)
	            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
                }
        }

        [HttpPost("")]
        public JsonResult Post(string scheduleName, [FromBody]CourseViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to entity
                    var newCourse = Mapper.Map<Course>(vm);

                    // Save to Database
                    _repository.AddCourse(scheduleName, newCourse);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CourseViewModel>(newCourse));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });

            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
