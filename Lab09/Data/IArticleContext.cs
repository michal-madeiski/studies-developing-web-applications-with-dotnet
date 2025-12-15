using Lab09.Models;

namespace Lab09.Data
{
    public interface IArticleContext
    {
        IEnumerable<ArticleViewModel> GetArticles();
        ArticleViewModel? GetArticle(int id);
        void AddArticle(ArticleViewModel article);
        void RemoveArticle(int id);
        void UpdateArticle(ArticleViewModel article);
    }
}