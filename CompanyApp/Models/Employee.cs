using System.ComponentModel.DataAnnotations;

namespace CompanyApp.Models
{
    public class Employee
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

        public Employee()
        {

        }
    }
}