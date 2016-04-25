using System;
using System.ComponentModel.DataAnnotations;

namespace Schedulizer_WebApp.ViewModels
{
    public class CourseViewModel
        // handles fields for course model
    {
        public int Id { get; set; }

        public string College { get; set; }

        public string Department { get; set; }

        public int CourseCode { get; set; }
    }
}