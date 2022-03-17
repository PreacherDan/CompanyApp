using System.ComponentModel.DataAnnotations;
using CompanyApp.DTOs;

namespace CompanyApp.Models
{
    public class Department
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int BudgetYearly { get; set; }

        public Department()
        {

        }

        public Department(DepartmentDTO departmentDTO)
        {
            this.ID = departmentDTO.ID;
            this.BudgetYearly = departmentDTO.BudgetYearly;
            this.Name = departmentDTO.Name;
            this.Location = departmentDTO.Location;
        }
    }
}
