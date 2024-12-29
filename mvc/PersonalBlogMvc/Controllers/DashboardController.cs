using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PersonalBlogMvc.Models;
using PersonalBlogMvc.Services;
using System.Reflection.Metadata;

namespace PersonalBlogMvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IArticleService _articleService;

        public DashboardController(ILogger<DashboardController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public IActionResult AddArticle()
        {
            ViewData["ShowGoBackButton"] = true;
            return View();
        }

        public IActionResult EditArticle(int id)
        {
            ViewData["ShowGoBackButton"] = true;

            var article = _articleService.GetArticleById(id);

            return View(new Views.Dashboard.EditArticleModel
            {
                Article = article
            });
        }

        public IActionResult Index()
        {
            ViewData["ShowAddButton"] = true;
            ViewData["ShowLogoutButton"] = true;
            ViewData["Articles"] = _articleService.GetArticles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                _articleService.CreateArticle(article);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                _articleService.UpdateArticle(article);  // Call the service to save the blog post
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Error");
        }

        public IActionResult DeleteArticle(int id)
        {
            var result = _articleService.DeleteArticle(id);
            return RedirectToAction("Index", "Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
