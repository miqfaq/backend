using Microsoft.EntityFrameworkCore;
using WebAPIBackend.Models.Users;

namespace WebAPIBackend.DbContexts
{
    public class AplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;

        public AplicationContext()
        {
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WebApiBackend;Username=postgres;Password=123");
        }
    }

}