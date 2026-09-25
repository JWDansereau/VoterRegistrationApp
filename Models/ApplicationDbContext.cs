using Microsoft.EntityFrameworkCore;

namespace VoterRegistrationApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<VoterRegistration> Registrations { get; set; }
    }
}