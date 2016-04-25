using System;
using System.ComponentModel.DataAnnotations;

namespace Schedulizer_WebApp.ViewModels
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255,MinimumLength = 5)]
        public string Name { get; set; }

        // Initialization of auto property
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}