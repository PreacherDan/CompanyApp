using System.ComponentModel.DataAnnotations;
using CompanyApp.Models;
using CompanyApp.DTOs;
using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models
{
    public class ConformsWithDeptBudget : ValidationAttribute
    {
        private EmployeeRepository employeeRepository = null;
        private DepartmentRepository departmentRepository = null;

        public ConformsWithDeptBudget()
        {
            employeeRepository = new EmployeeRepository(new HttpClient() { BaseAddress = new Uri(@"https://localhost:7235")});
            departmentRepository = new DepartmentRepository(new HttpClient() { BaseAddress= new Uri(@"https://localhost:7235") });
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Employee employee = null;
            Department department = null;

            if (validationContext.ObjectInstance is Employee) employee = (Employee)validationContext.ObjectInstance;
            else if (validationContext.ObjectInstance is EmployeeDTO) employee = new Employee((EmployeeDTO)validationContext.ObjectInstance);
            if (employee == null) return new ValidationResult("Object is not an employee");

            department = new Department(departmentRepository.GetDepartment(employee.DepartmentID));
            department.Employees = employeeRepository.GetEmployees()
                .Where(e => e.DepartmentID == department.ID)
                .ToList<EmployeeDTO>()
                .Select(e => new Employee(e))
                .ToList<Employee>();

            //Int32.TryParse(value, out newSalary);

            var empOldSalary = employeeRepository.GetEmployee(employee.ID).Salary;
            int? currentSalaries = 0;
            var oldDeptID = employeeRepository.GetEmployee(employee.ID).DepartmentID;

            if (oldDeptID == employee.DepartmentID)
                currentSalaries = department.Employees.Sum(e => e.Salary) - empOldSalary;
            else
                currentSalaries = department.Employees.Sum(e => e.Salary);

            if ((currentSalaries * 12) + (Convert.ToInt32(value)* 12) > department.BudgetYearly)
                return new ValidationResult($"'{employee.Salary}' salary would exceed yearly '{department.Name}' Department budget ('{department.BudgetYearly}')");

            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }
    }
}


//employee = (validationContext.ObjectInstance is Employee) ? (Employee)value : new Employee((EmployeeDTO)value);
//department = context.Departments.Include(d => d.Employees).Single(d => d.ID == employee.DepartmentID);