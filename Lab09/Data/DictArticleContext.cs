using Lab09.Models;

namespace Lab09.Data
{
    public class DictArticleContext : IArticleContext
    {
        Dictionary<int, ArticleViewModel> art = new Dictionary<int, ArticleViewModel>()
        {
            { 0, new ArticleViewModel(0, "Milk", 1.5m, DateTime.Now.AddDays(7), Category.Dairy) },
            { 1, new ArticleViewModel(1, "Bread", 2.0m, DateTime.Now.AddDays(3), Category.Bakery) },
            { 2, new ArticleViewModel(2, "Apple", 0.5m, DateTime.Now.AddDays(10), Category.Fruit) },
        };

        public IEnumerable<ArticleViewModel> GetArticles()
        {
            return art.Values;
        }

        public ArticleViewModel? GetArticle(int id)
        {
            art.TryGetValue(id, out var article);
            return article;
        }

        public void AddArticle(ArticleViewModel article)
        {
            int nextId = art.Keys.Any() ? art.Keys.Max() + 1 : 0;
            article.Id = nextId;
            art[article.Id] = article;
        }

        public void RemoveArticle(int id)
        {
            art.Remove(id);
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            if (art.ContainsKey(article.Id))
            {
                art[article.Id] = article;
            }
        }
    }
}