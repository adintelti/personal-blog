using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PersonalBlogMvc.Models;
using PersonalBlogMvc.Services;
using PersonalBlogMvc.Views.Home;

namespace PersonalBlogMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            ViewData["Articles"] = _articleService.GetArticles();
            return View();
        }

        public IActionResult Article(int id)
        {
            ViewData["ShowGoBackButton"] = true;
            var article = _articleService.GetArticleById(id);
            var articleModel = new ArticleModel
            {
                Article = article
            };
            return View(articleModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
