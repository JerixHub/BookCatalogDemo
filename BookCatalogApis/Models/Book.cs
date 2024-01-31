using System.ComponentModel.DataAnnotations;

namespace BookCatalogApis.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime PublishDateUtc { get; set; }
        public Category? category { get; set; }
    }
}
