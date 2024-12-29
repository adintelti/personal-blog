using PersonalBlogMvc.Models;
using System.Reflection.Metadata;
using System.Text.Json;

namespace PersonalBlogMvc.Services
{
    public class ArticleService : IArticleService
    {
        public bool CreateArticle(ArticleViewModel article)
        {
            try
            {
                Random rnd = new Random();
                int id = rnd.Next(0001, 9999);
                string fileName = GetFileName(id);
                string filePath = Path.Combine(GetDataFolder(), fileName);

                WriteFile(article, id, fileName, filePath);
                Console.WriteLine($"File {fileName} created successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Service : {nameof(CreateArticle)} " + ex.Message);
                return false;
            }
        }

        private static void WriteFile(ArticleViewModel article, int id, string fileName, string filePath)
        {
            CreateFile(fileName, filePath);
            var articleToAdd = new ArticleViewModel(id, article.ArticleTitle, article.ArticlePublishDate, article.ArticleContent);
            var fileStream = JsonSerializer.Serialize<ArticleViewModel>(articleToAdd);
            File.WriteAllText(filePath, fileStream);
        }

        public bool DeleteArticle(int id)
        {
            try
            {
                string fileName = GetFileName(id);
                string filePath = Path.Combine(GetDataFolder(), fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"File {fileName} deleted successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine($"File {fileName} doesn't exists");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Service : {nameof(DeleteArticle)} " + ex.Message);
                return false;
            }
        }

        public ArticleViewModel GetArticleById(int id)
        {
            try
            {
                string fileName = GetFileName(id);
                string filePath = Path.Combine(GetDataFolder(), fileName);
                if (File.Exists(filePath))
                {
                    var fileData = File.ReadAllText(filePath);
                    var post = JsonSerializer.Deserialize<ArticleViewModel>(fileData);
                    return post ?? new();
                }
                else
                {
                    Console.WriteLine($"File {fileName} doesn't exists");
                    return new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Service : {nameof(GetArticleById)} " + ex.Message);
                return new();
            }
        }

        public List<ArticleViewModel> GetArticles()
        {
            List<ArticleViewModel> dataList = new List<ArticleViewModel>();
            string directoryPath = GetDataFolder();
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string[] articleFiles = Directory.GetFiles(directoryPath, "*.json");
            foreach (string articleFile in articleFiles)
            {
                string jsonContent = File.ReadAllText(articleFile);
                var data = JsonSerializer.Deserialize<ArticleViewModel>(jsonContent);
                dataList.Add(data);
            }

            return dataList.OrderBy(x => x.ArticlePublishDate).ToList();
        }

        public bool UpdateArticle(ArticleViewModel article)
        {
            try
            {
                string fileName = GetFileName(article.ArticleId);
                string filePath = Path.Combine(GetDataFolder(), fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);

                    WriteFile(article, article.ArticleId, fileName, filePath);
                    Console.WriteLine($"File {fileName} updated successfully");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Service : {nameof(UpdateArticle)} " + ex.Message);
                return false;
            }
        }

        private static void CreateFile(string fileName, string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Console.WriteLine($"File {fileName} created successfully.");
                }
            }
        }

        private string GetFileName(int id)
        {
            return $"{id}.json";
        }

        private string GetDataFolder()
        {
            return Directory.GetCurrentDirectory() + "\\Data";
        }
    }
}
