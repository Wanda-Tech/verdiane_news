using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Data
{
    // Junction Table: Implements many-to-many relationship between Users and Roles
    // This allows one user to have multiple roles, and one role to be assigned to multiple users
    public class UserRole
    {
        // Primary Key: Maps to id in database
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Foreign Key to User table
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        // Navigation Property to User
        public virtual User User { get; set; } = null!;

        // Foreign Key to Role table
        [Required]
        [Column("role_id")]
        public int RoleId { get; set; }

        // Navigation Property to Role
        public virtual Role Role { get; set; } = null!;
    }
}