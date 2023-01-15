using Microsoft.EntityFrameworkCore;
using AuthService.Database.Models;
using System.Net;

namespace AuthService.Database
{
    public class AuthDbContext : DbContext
    {
        internal DbSet<User> Users { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
        {
        }

    }
}
