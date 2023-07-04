using FCI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FCI_API.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the collection of Post entities.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}