namespace Lab10.Models
{
    public class CartViewModel
    {
        public List<Article> Articles { get; set; } = new();
        public Dictionary<int, int> Quantities { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
