using NewsWebsite.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Models
{
    public class NewsCategory
    {
        // Primary Key: Maps to id in database
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Category name - maximum 100 characters, cannot be null
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Column("name")]
        public string Name { get; set; }

        // Category description - maximum 255 characters, cannot be null
        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [Column("description")]
        public string Description { get; set; }

        // Navigation Property: All news articles in this category
        public virtual ICollection<News> NewsList { get; set; } = new List<News>();
    }
}