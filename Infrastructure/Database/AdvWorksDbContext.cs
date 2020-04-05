using Domain;
using Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AdvWorksDbContext : DbContext
    {
        private const string ConnectionString =
            "Server=DESKTOP-J01R736;Database=AdventureWorks2016CTP3;Trusted_Connection=True;";

        public AdvWorksDbContext(DbContextOptions<AdvWorksDbContext> options)
            : base(options)
        {
            // здесь например, можно установить timeout исполнения запросов
            // Database.SetCommandTimeout(60);
        }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
