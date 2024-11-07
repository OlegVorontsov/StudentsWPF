using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentsWPF.Models;

namespace StudentsWPF.DataAccess
{
    public class ApplicationDbContext() : DbContext
    {
        private const string connectionString = "Host=localhost;Port=5436;User ID=postgres;Password=123;Database=StudentsDb;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Grade> Grades => Set<Grade>();
    }
}
