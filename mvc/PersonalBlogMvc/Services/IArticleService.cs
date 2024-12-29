using PersonalBlogMvc.Models;
using System.Reflection.Metadata;

namespace PersonalBlogMvc.Services
{
    public interface IArticleService
    {
        bool CreateArticle(ArticleViewModel article);
        bool DeleteArticle(int id);
        bool UpdateArticle(ArticleViewModel article);
        ArticleViewModel GetArticleById(int id);
        List<ArticleViewModel> GetArticles();
    }
}
