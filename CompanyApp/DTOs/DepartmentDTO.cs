using CompanyApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace CompanyApp.DTOs
{
    public class DepartmentDTO
    {
        //[Required(ErrorMessage ="Department ID Required")]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int BudgetYearly { get; set; }
        public ICollection<EmployeeDTO> Employees { get; set; }

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

            //this.Employees = department.Employees;
        }
    }
}
