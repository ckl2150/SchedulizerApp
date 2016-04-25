using System.Collections.Generic;

namespace Schedulizer_WebApp.Models
{
    public interface ICourseRepository
    {
        IEnumerable<Schedule> GetAllSchedules();
        IEnumerable<Schedule> GetAllScheduleswithCourses();
        void AddSchedule(Schedule newSchedule);
        bool SaveAll();
        Schedule GetScheduleByName(string scheduleName);
        void AddCourse(string scheduleName,  Course newCourse);
    }
}