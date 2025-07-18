using NewsWebsite.Models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database schema attributes

namespace NewsWebsite.Models
{
    public class News
    {
        // Primary Key: Maps to news_id in database
        [Key]
        [Column("news_id")]
        public int NewsId { get; set; }

        // Title of the news article - maximum 100 characters
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [Column("title")]
        public string Title { get; set; }

        // Content of the news article - using TEXT data type in SQL Server
        [Required(ErrorMessage = "Content is required")]
        [Column("content", TypeName = "TEXT")] // Specifies SQL Server data type
        public string Content { get; set; }

        // When the news was created - can be null
        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        // Foreign Key: Links to the User table (owner/creator of the news)
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        // News status using enum - default value is 0 (Draft)
        [Required]
        [Column("NewsStatus")]
        public NewsStatus NewsStatus { get; set; } = NewsStatus.Draft;

        // When the news should be published - can be null for drafts
        [Column("published_date")]
        public DateTime? PublishedDate { get; set; }

        // Brief summary or excerpt of the news - can be null
        [Column("summary", TypeName = "TEXT")]
        public string? Summary { get; set; }

        // Foreign Key: Links to the NewsCategory table
        [Required]
        [Column("news_category_id")]
        public int NewsCategoryId { get; set; }

        // Path to the featured image - can be null, maximum 100 characters
        [StringLength(100)]
        [Column("image_url")]
        public string? ImageUrl { get; set; }

        // Navigation Property: Allows accessing the related User object (creator)
        public virtual User User { get; set; } = null!;

        // Navigation Property: Allows accessing the related NewsCategory object
        public virtual NewsCategory NewsCategory { get; set; } = null!;
    }

    // Enum: Defines possible news statuses as per your schema
    public enum NewsStatus
    {
        Draft = 0,     // News is being written
        Published = 1, // News is live on the website
        Removed = 2    // News has been removed/archived
    }
}