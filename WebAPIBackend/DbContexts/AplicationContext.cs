using Microsoft.EntityFrameworkCore;
using WebAPIBackend.Models.Departments;
using WebAPIBackend.Models.Employees;
using WebAPIBackend.Models.Tools;
using WebAPIBackend.Models.Users;

namespace WebAPIBackend.DbContexts
{
    public class AplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Department> Departments { get; set; } = null;
        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Tool> Tools { get; set; } = null;
        public DbSet<WorkTime> WorkTime { get; set; } = null;
        public DbSet<UserFile> UserFiles { get; set; } = null;


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