using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab10.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must contain minimum 2 characters.")]
        [MaxLength(30, ErrorMessage = "Name must contain maximum 30 characters.")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1.00, 10000.00, ErrorMessage = "Price must between {1} and {2}.")]

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? FormImage { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
