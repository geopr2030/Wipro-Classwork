using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment2_Razor.Models;

namespace Assignment2_Razor.Pages
{
    public class RegisterModel : PageModel
    {
        // static list (temporary storage)
        public static List<Student> Students = new List<Student>();

        [BindProperty]
        public Student Student { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Students.Add(Student);

            return RedirectToPage("Students");
        }
    }
}