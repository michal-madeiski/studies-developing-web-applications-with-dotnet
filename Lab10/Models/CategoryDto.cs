using System.ComponentModel.DataAnnotations;

namespace Lab10.Models
{
    public class CategoryDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
