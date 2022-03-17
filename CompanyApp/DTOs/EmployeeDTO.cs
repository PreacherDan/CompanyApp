using CompanyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.DTOs
{
    public class EmployeeDTO
    {
        [Required]
        public int? ID { get; set; } // nullable so ID doesnt get automatically initialized with default value (0 or read as empty string my ModelState.IsValid checker)

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        //[ConformsWithBudget] // custom validation class
        public int? Salary { get; set; }

        public bool IsOnLeave { get; set; }

        // navigation property
        public Department Department { get; set; }
        //public byte DepartmentID { get; set; } // not required in newer convention !

        public EmployeeDTO()
        {

        }

        public EmployeeDTO(Employee employee)
        {
            this.ID = employee.ID;
            this.Name = employee.Name;
            this.Surname = employee.Surname;
            this.Salary = employee.Salary;
            this.Department = employee.Department;
            this.IsOnLeave = employee.IsOnLeave;
        }
    }
}
