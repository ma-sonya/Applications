using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace chern1.Models
{
    public class WorkContext: DbContext
    {
        public WorkContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = LAPTOP-987VEQHD; Database =Test_WorkDB; Trusted_Connection=True");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Regular> Regulars { get; set; }
    }
}
