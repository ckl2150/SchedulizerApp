using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
