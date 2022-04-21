using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CompanyApp.Models;
using CompanyApp.DTOs;

namespace CompanyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public static DbContextOptions Options { get; private set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :   base(options)
        {
            ApplicationDbContext.Options = options;
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

            builder.Entity<Employee>().HasData(
                new Employee() { ID = 1, DepartmentID = 1, Salary = 5000, IsOnLeave = false, Name = "Jan", Surname = "Kowalski" },
                new Employee() { ID = 2, DepartmentID = 4, Salary = 6000, IsOnLeave = false, Name = "Piotr", Surname = "Nowak" },
                new Employee() { ID = 3, DepartmentID = 1, Salary = 3000, IsOnLeave = false, Name = "Forrest", Surname = "Gump" }
                );
        }

        public override void Dispose()
        {   
            base.Dispose();
        }
    }
}