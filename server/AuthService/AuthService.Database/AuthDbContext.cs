using Microsoft.EntityFrameworkCore;
using AuthService.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthService.Database
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        //internal DbSet<User> Users { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
