using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // foreign key attr
using CompanyApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models
{
    public class Employee
    {
        [Required]
        public int ID { get; set; } 
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [Required]
        [ConformsWithDeptBudget] // custom validation class
        public int? Salary { get; set; } // nullable so salary doesnt get automatically initialized with default value (0 or read as empty string my ModelState.IsValid checker)

        [Display(Name="Is On Leave?")]
        public bool IsOnLeave { get; set; }

        // navigation property
        public Department Department { get; set; }

        //[ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; } // not required in newer convention !
        

        public Employee()
        {

        }

        public Employee(EmployeeDTO employeeDTO)
        {
            this.ID = employeeDTO.ID;
            this.Name = employeeDTO.Name;
            this.Surname = employeeDTO.Surname;
            this.Salary = employeeDTO.Salary;
            this.IsOnLeave = employeeDTO.IsOnLeave;

            if (employeeDTO.Department != null) this.Department = new Department(employeeDTO.Department);

            this.DepartmentID = employeeDTO.DepartmentID;
        }
    }
}