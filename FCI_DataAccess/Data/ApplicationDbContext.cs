using FCI_API.Models;
using FCI_DataAccess.Models;
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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectDepartment> SubjectDepartments { get; set; }
        public DbSet<ContactUsMessage> ContactUsMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Subject and Department
            modelBuilder.Entity<SubjectDepartment>()
                .HasKey(sd => new { sd.SubjectId, sd.DepartmentId });

            modelBuilder.Entity<SubjectDepartment>()
                .HasOne(sd => sd.Subject)
                .WithMany(s => s.SubjectDepartments)
                .HasForeignKey(sd => sd.SubjectId);

            modelBuilder.Entity<SubjectDepartment>()
                .HasOne(sd => sd.Department)
                .WithMany(d => d.SubjectDepartments)
                .HasForeignKey(sd => sd.DepartmentId);
        }


    }
}