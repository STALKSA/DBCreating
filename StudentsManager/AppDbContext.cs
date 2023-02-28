using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;

namespace StudentsManager
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=\"C:\\Users\\Ksy18\\OneDrive\\Рабочий стол\\ADO\\StudentsManager\\StudentsManager\\hello.db\"";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Visit> Visits => Set<Visit>();
    }

}
