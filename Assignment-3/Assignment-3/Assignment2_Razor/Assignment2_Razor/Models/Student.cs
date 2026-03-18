using System.ComponentModel.DataAnnotations;

namespace Assignment2_Razor.Models
{
    public class Student
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }
    }
}