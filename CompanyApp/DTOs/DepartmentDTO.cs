using CompanyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.DTOs
{
    public class DepartmentDTO
    {
        //[Required(ErrorMessage ="Department ID Required")]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int BudgetYearly { get; set; }

        public DepartmentDTO()
        {
        }

        public DepartmentDTO(Department department)
        {
            this.ID = department.ID;
            this.BudgetYearly = department.BudgetYearly;
            this.Name = department.Name;
            this.Location = department.Location;
        }
    }
}
