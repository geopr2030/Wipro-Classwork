using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment2_Razor.Models;

namespace Assignment2_Razor.Pages
{
    public class StudentsModel : PageModel
    {
        public List<Student> StudentList { get; set; }

        public void OnGet()
        {
            StudentList = RegisterModel.Students;
        }
    }
}