using FCI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FCI_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

    }
}
