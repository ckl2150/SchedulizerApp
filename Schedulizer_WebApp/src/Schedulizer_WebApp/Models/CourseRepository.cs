using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Models
{
    public class CourseRepository : ICourseRepository
    {
        private CourseContext _context;
        private ILogger<CourseRepository> _logger;

        public CourseRepository(CourseContext context, ILogger<CourseRepository> logger)
            {
                _context = context;
                _logger = logger;
            }

        public void AddCourse(string scheduleName, Course newCourse)
        {
            var theSchedule = GetScheduleByName(scheduleName);
            _context.Courses.Add(newCourse);
        }

        public void AddSchedule(Schedule newSchedule)
        {
            _context.Add(newSchedule);
        }

        public IEnumerable<Schedule> GetAllSchedules()
            {
                try
                {
                    return _context.Schedules.OrderBy(t => t.Name).ToList();
                }
                catch(Exception ex)
                {
                    _logger.LogError("Could not get schedules from database", ex);
                    return null;
                }
            }
            public IEnumerable<Schedule> GetAllScheduleswithCourses()
            {
                try
                {
                    return _context.Schedules
                        .Include(t => t.Courses)
                        .OrderBy(t => t.Name)
                        .ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Could not get courses from database", ex);
                    return null;
                }
            

            }

        public Schedule GetScheduleByName(string scheduleName)
        {
            return _context.Schedules.Include(t => t.Courses)
                .Where(t => t.Name == scheduleName)
                .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
