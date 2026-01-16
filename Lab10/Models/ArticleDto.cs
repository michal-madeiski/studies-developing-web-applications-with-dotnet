using System.ComponentModel.DataAnnotations;

namespace Lab10.Models
{
    public class ArticleDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
