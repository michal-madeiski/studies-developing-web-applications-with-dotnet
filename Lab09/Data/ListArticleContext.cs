using Lab09.Models;

namespace Lab09.Data
{
    public class ListArticleContext : IArticleContext
    {
        List<ArticleViewModel> art = new List<ArticleViewModel>()
        {
            new ArticleViewModel(0, "Milk", 1.5m, DateTime.Now.AddDays(7), Category.Dairy),
            new ArticleViewModel(1, "Bread", 2.0m, DateTime.Now.AddDays(3), Category.Bakery),
            new ArticleViewModel(2, "Apple", 0.5m, DateTime.Now.AddDays(10), Category.Fruit),
        };

        public IEnumerable<ArticleViewModel> GetArticles()
        {
            return art;
        }

        public ArticleViewModel? GetArticle(int id)
        {
            return art.FirstOrDefault(a => a.Id == id);
        }

        public void AddArticle(ArticleViewModel article)
        {
            int nextId = art.Any() ? art.Max(a => a.Id) + 1 : 0;
            article.Id = nextId;
            art.Add(article);
        }

        public void RemoveArticle(int id)
        {
            ArticleViewModel? artToRemove = art.FirstOrDefault(a => a.Id == id);
            if (artToRemove != null)
                art.Remove(artToRemove);
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            art = art.Select(a => (a.Id == article.Id) ? article : a).ToList();
        }
    }
}