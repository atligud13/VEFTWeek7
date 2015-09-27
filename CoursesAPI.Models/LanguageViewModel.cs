using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoursesAPI.Models
{
    public class LanguageViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}