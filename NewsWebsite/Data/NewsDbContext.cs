using NewsWebsite.Models; // Your entity models
using Microsoft.EntityFrameworkCore; // Main EF Core namespace

namespace NewsWebsite.Data
{
    // DbContext: The main class that coordinates Entity Framework functionality for a data model
    public class AppDbContext : DbContext
    {
        // Constructor: Receives configuration options (connection string, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // The base constructor passes options to the parent DbContext class
        }

        // DbSet: Represents a table in the database
        // Each DbSet property corresponds to a table in SQL Server
        public DbSet<News> News { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<NewsCategory> NewsCategories { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;

        // OnModelCreating: Configures the database model using Fluent API
        // This method is called when the model is first created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base implementation first
            base.OnModelCreating(modelBuilder);

            // Configure table names to match your database schema
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<NewsCategory>().ToTable("NewsCategories");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");

            // Configure News -> User relationship (Many-to-One)
            modelBuilder.Entity<News>()
                .HasOne(n => n.User) // One user (creator)
                .WithMany(u => u.NewsList) // Many news articles
                .HasForeignKey(n => n.UserId) // Foreign key
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting user if they have news articles

            // Configure News -> NewsCategory relationship (Many-to-One)
            modelBuilder.Entity<News>()
                .HasOne(n => n.NewsCategory) // One category
                .WithMany(nc => nc.NewsList) // Many news articles
                .HasForeignKey(n => n.NewsCategoryId) // Foreign key
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting category if it has news articles

            // Configure UserRole -> User relationship (Many-to-One)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User) // One user
                .WithMany(u => u.UserRoles) // Many user roles
                .HasForeignKey(ur => ur.UserId) // Foreign key
                .OnDelete(DeleteBehavior.Cascade); // Delete user roles when user is deleted

            // Configure UserRole -> Role relationship (Many-to-One)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role) // One role
                .WithMany(r => r.UserRoles) // Many user roles
                .HasForeignKey(ur => ur.RoleId) // Foreign key
                .OnDelete(DeleteBehavior.Cascade); // Delete user roles when role is deleted

            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Ensure email addresses are unique

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(); // Ensure usernames are unique

            modelBuilder.Entity<NewsCategory>()
                .HasIndex(nc => nc.Name)
                .IsUnique(); // Ensure category names are unique

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique(); // Ensure role names are unique

            // Configure default values
            modelBuilder.Entity<News>()
                .Property(n => n.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()"); // SQL Server function for current UTC date

            modelBuilder.Entity<User>()
                .Property(u => u.Created)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<News>()
                .Property(n => n.NewsStatus)
                .HasDefaultValue(NewsStatus.Draft); // Default status to Draft

            // Seed data (optional): Add initial data to the database
            SeedData(modelBuilder);
        }

        // Method to seed initial data into the database
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed NewsCategories
            modelBuilder.Entity<NewsCategory>().HasData(
                new NewsCategory { Id = 1, Name = "Articles", Description = "General news articles and stories" },
                new NewsCategory { Id = 2, Name = "Reports", Description = "Detailed reports and investigations" },
                new NewsCategory { Id = 3, Name = "Breaking", Description = "Breaking news and urgent updates" }
            );

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "admin", Description = "Full system access and management" },
                new Role { RoleId = 2, Name = "editor", Description = "Can create, edit, and manage news content" }
            );

            // ✅ Use fixed DateTime value here
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    Email = "admin@affaznews.com",
                    Phone = "1234567890",
                    Password = "admin123", // ⚠️ This should be hashed in production
                    Created = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1 }
            );
        }
    }
}