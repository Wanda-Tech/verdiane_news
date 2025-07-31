using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Data
{
    public class Role
    {
        // Primary Key: Maps to role_id in database
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        // Role name - maximum 100 characters, cannot be null
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(100, ErrorMessage = "Role name cannot exceed 100 characters")]
        [Column("name")]
        public required string Name { get; set; }

        // Role description - maximum 150 characters, cannot be null
        [Required(ErrorMessage = "Description is required")]
        [StringLength(150, ErrorMessage = "Description cannot exceed 150 characters")]
        [Column("description")]
        public required string Description { get; set; }

        // Navigation Property: All users assigned to this role (many-to-many relationship)
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}