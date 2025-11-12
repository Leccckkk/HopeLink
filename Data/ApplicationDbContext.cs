using Microsoft.EntityFrameworkCore;
using WebsiteCharity.Models;

namespace WebsiteCharity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
    }
}
