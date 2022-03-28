using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CompanyApp.Models;

namespace CompanyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>().HasData(               
                new Department() { ID = 1, Name = "IT", BudgetYearly = 750000, Location = "Cracow" },
                new Department() { ID = 2, Name = "R&D", BudgetYearly = 1000000, Location = "Warsaw" },
                new Department() { ID = 3, Name = "Management", BudgetYearly = 80000, Location = "Cracow" },
                new Department() { ID = 4, Name = "Finance", BudgetYearly = 500000, Location = "Warsaw" }
                );
        }
    }
}