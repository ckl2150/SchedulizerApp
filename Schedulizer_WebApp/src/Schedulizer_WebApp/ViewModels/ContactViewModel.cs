using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Schedulizer_WebApp.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255, MinimumLength =5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(1024, MinimumLength =5)]
        public string Message { get; set; }
    }
}
