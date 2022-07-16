using Microsoft.EntityFrameworkCore;

namespace DrugstoreAPIWebApp.Models
{
    public class DrugstoreAPIContext: DbContext
    {
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ProducerDrug> ProducerDrugs { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public DrugstoreAPIContext(DbContextOptions<DrugstoreAPIContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
