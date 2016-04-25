using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Models
{
    public class CourseContextSeedData
    {
        private CourseContext _context;
        
        public CourseContextSeedData(CourseContext context)
        {
            _context = context;
        }

        public void EnsureSeedData()
        {
            if(!_context.Schedules.Any())
            {
                // Add new data
                var fallSchedule = new Schedule()
                {
                    Name = "Fall Schedule",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Courses = new List<Course>()
                    {
                        new Course() { College = "ENG", Department="EK", CourseCode=127 },
                        new Course() { College = "ENG", Department = "EC", CourseCode = 327 },
                        new Course() { College = "ENG", Department = "EC", CourseCode = 330 },
                        new Course() { College = "ENG", Department = "EC", CourseCode = 521 },

                    }
                };

                _context.Schedules.Add(fallSchedule);
                _context.Courses.AddRange(fallSchedule.Courses);

                _context.SaveChanges();
            }
        }
    }

    
}
