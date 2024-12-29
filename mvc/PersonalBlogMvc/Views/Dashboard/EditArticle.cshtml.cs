using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlogMvc.Models;

namespace PersonalBlogMvc.Views.Dashboard
{
    public class EditArticleModel : PageModel
    {
        [BindProperty]
        public ArticleViewModel Article { get; set; }
        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Dashboard");
        }
    }
}
