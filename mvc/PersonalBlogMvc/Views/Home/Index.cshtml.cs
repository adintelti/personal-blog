using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlogMvc.Models;
using System.Reflection.Metadata;

namespace PersonalBlogMvc.Views.Home
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public ArticleViewModel Article { get; set; }

        public void OnGet()
        {
        }
    }
}
