using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlogMvc.Models;
using System.Reflection.Metadata;

namespace PersonalBlogMvc.Views.Dashboard
{
    public class AddArticleModel : PageModel
    {
        [BindProperty]
        public ArticleViewModel Article { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
