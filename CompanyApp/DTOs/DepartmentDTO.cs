using CompanyApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace CompanyApp.DTOs
{
    public class DepartmentDTO
    {
        //[Required(ErrorMessage ="Department ID Required")]
        public int ID { get; set; } // set nullable: not initializing with default value and displaying it in form
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? BudgetYearly { get; set; }
        
        public ICollection<EmployeeDTO> Employees { get; set; }
        public int EmployeeCount { get; private set; }
        public int? SpendingYearly { get; private set; }

        public DepartmentDTO()
        {
            Employees = new Collection<EmployeeDTO>();
        }

        public DepartmentDTO(Department department)
        {
            this.ID = department.ID;
            this.BudgetYearly = department.BudgetYearly;
            this.Name = department.Name;
            this.Location = department.Location;

            if (department.Employees != null)
            {
                this.EmployeeCount = department.Employees.Count;
                this.SpendingYearly = department.Employees.Sum(e => e.Salary * 12);
            }
            else this.EmployeeCount = 0;
        }
    }
}
