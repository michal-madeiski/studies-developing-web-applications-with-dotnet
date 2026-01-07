using System.ComponentModel.DataAnnotations;

namespace Lab11.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must contain minimum 2 characters.")]
        [MaxLength(30, ErrorMessage = "Name must contain maximum 30 characters.")]
        public string? Name { get; set; }

        public ICollection<Article>? Articles { get; set; }
    }
}
