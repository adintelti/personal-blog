namespace PersonalBlogMvc.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public DateTime ArticlePublishDate { get; set; }

        public string ArticleContent { get; set; }

        public ArticleViewModel(int id, string title, DateTime publishTime, string content)
        {
            ArticleId = id;
            ArticleTitle = title;
            ArticlePublishDate = publishTime;
            ArticleContent = content;
        }

        public ArticleViewModel()
        {
                
        }
    }
}
