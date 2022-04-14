using CompanyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.DTOs
{
    public class EmployeeDTO
    {
        [Required(ErrorMessage ="Employee ID Required")]
        public int ID { get; set; } // nullable so ID doesnt get automatically initialized with default value (0 or read as empty string my ModelState.IsValid checker)

        [Required(ErrorMessage ="Employee Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee Surname Required")]
        public string Surname { get; set; }

        //[ConformsWithBudget] // custom validation class
        [Required(ErrorMessage = "Employee Salary Required")]
        public int? Salary { get; set; }

        [Display(Name="Is On Leave?")]
        public bool IsOnLeave { get; set; }

        // navigation property
        public DepartmentDTO? Department { get; set; }
        //public byte DepartmentID { get; set; } // not required in newer convention !

        public int DepartmentID { get; set; }

        public EmployeeDTO()
        {

        }

        public EmployeeDTO(Employee employee)
        {
            this.ID = employee.ID;
            this.Name = employee.Name;
            this.Surname = employee.Surname;
            this.Salary = employee.Salary;
            this.Department = new DepartmentDTO(employee.Department);
            this.IsOnLeave = employee.IsOnLeave;

            this.DepartmentID = employee.DepartmentID;
        }
    }
}
