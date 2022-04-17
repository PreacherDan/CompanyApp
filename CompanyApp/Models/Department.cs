using System.ComponentModel.DataAnnotations;
using CompanyApp.DTOs;
using System.Collections.ObjectModel;

namespace CompanyApp.Models
{
    public class Department
    {
        //[Required]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int BudgetYearly { get; set; }
        public ICollection<Employee> Employees { get; set; }
        //public Employee Manager { get; set; }

        public Department()
        {
            this.Employees = new Collection<Employee>();
        }

        public Department(DepartmentDTO departmentDTO)
        {
            this.ID = departmentDTO.ID;
            this.BudgetYearly = departmentDTO.BudgetYearly;
            this.Name = departmentDTO.Name;
            this.Location = departmentDTO.Location;

            if(departmentDTO.Employees!=null && departmentDTO.Employees.Any())
                this.Employees = departmentDTO.Employees.Select(e => new Employee(e)).ToList<Employee>();
        }
    }
}
