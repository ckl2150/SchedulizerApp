using System;
using Microsoft.Data.Entity;

namespace Schedulizer_WebApp.Models
{
    public class CourseContext : DbContext
    {
        public CourseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:CourseContextConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
