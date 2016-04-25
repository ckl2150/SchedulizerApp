using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Schedulizer_WebApp.Services;
using Schedulizer_WebApp.ViewModels;
using Schedulizer_WebApp.Models;

namespace Schedulizer_WebApp.Controllers.Web
{
    public class AppController : Controller
    {
        private ICourseRepository _repository;

        public AppController(ICourseRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var schedules = _repository.GetAllSchedules();

            return View(schedules);
        }

        public IActionResult About()
        {
            return View();
        }

    }
}

