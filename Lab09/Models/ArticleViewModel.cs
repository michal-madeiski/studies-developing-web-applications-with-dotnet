using System.ComponentModel.DataAnnotations;

namespace Lab09.Models
{
    public enum Category { Dairy, Bakery, Vegetable, Fruit, Meat, }
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage= "Name must contain minimum 2 characters.")]
        [MaxLength(30, ErrorMessage= "Name must contain maximum 30 characters.")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 10000.00, ErrorMessage = "Price must between {1} and {2}.")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public Category Category { get; set; }


        public ArticleViewModel() { }

        public ArticleViewModel(int id, string? name, decimal price, DateTime expiryDate, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpiryDate = expiryDate;
            Category = category;
        }

        public ArticleViewModel(string? name, decimal price, DateTime expiryDate, Category category)
        {
            Name = name;
            Price = price;
            ExpiryDate = expiryDate;
            Category = category;
        }
    }
}