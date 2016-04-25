using AutoMapper;
using Microsoft.AspNet.Mvc;
using Schedulizer_WebApp.Models;
using Schedulizer_WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Controllers.Api
{
    [Route("api/schedules")]
    public class ScheduleController : Controller
    {
        private ICourseRepository _repository;

        public ScheduleController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = Mapper.Map<IEnumerable<ScheduleViewModel>>(_repository.GetAllScheduleswithCourses());
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]ScheduleViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newSchedule = Mapper.Map<Schedule>(vm);

                    // Save to Database
                    _repository.AddSchedule(newSchedule);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<ScheduleViewModel>(newSchedule)); 
                    }
                }
            }
            catch (Exception ex)
	        {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message});

            }

            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState});
        }
    }
}
