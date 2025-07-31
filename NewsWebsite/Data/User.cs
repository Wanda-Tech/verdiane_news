using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Data
{
    public class User
    {
        // Primary Key: Maps to user_id in database
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        // User's name - maximum 100 characters, cannot be null
        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "User name cannot exceed 100 characters")]
        [Column("user_name")]
        public required string UserName { get; set; }

        // User's phone number - maximum 10 characters, cannot be null
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, ErrorMessage = "Phone number cannot exceed 10 characters")]
        [Column("phone")]
        public required string Phone { get; set; }

        // User's email address - maximum 50 characters, cannot be null
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        [Column("email")]
        public required string Email { get; set; }

        // When the user account was created - cannot be null
        [Required]
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // User's password - maximum 100 characters, cannot be null
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        [Column("password")]
        public required string Password { get; set; }

        // Navigation Property: All news articles created by this user
        public virtual ICollection<News> NewsList { get; set; } = new List<News>();

        // Navigation Property: All roles assigned to this user (many-to-many relationship)
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}