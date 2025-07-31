using Microsoft.EntityFrameworkCore;
using NewsWebsite.Data;
using NewsWebsite.Models;
namespace NewsWebsite.Models
{
    public class AppDbContext :
 DbContext
    {
        public
            AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
