using System.ComponentModel.DataAnnotations;
using CompanyApp.Models;
using CompanyApp.DTOs;
using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models
{
    /// <summary>
    /// This validation attribute confirms the ability to assign an Employee to the selected department by checking whether his salary wouldn't cross
    /// the department's yearly budget.
    /// </summary>
    public class ConformsWithDeptBudget : ValidationAttribute
    {
        private EmployeeRepository employeeRepository = null;
        private DepartmentRepository departmentRepository = null;

        public ConformsWithDeptBudget()
        {
            // ---- TEMPORARY ----
            // enter actual endpoint Uri after deployment!!
            employeeRepository = new EmployeeRepository(new HttpClient() { BaseAddress = new Uri(@"https://localhost:7235") });
            departmentRepository = new DepartmentRepository(new HttpClient() { BaseAddress= new Uri(@"https://localhost:7235") });
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Employee employee = null;
            Department department = null;

            // unbox the Employee or EmployeeDTO object from the ValidationContext object
            if (validationContext.ObjectInstance is Employee) employee = (Employee)validationContext.ObjectInstance;
            else if (validationContext.ObjectInstance is EmployeeDTO) employee = new Employee((EmployeeDTO)validationContext.ObjectInstance);
            if (employee == null) return new ValidationResult("Object is not an employee");

            try
            {
                // Extract the department object from the DB, along with all it's Employees
                department = new Department(departmentRepository.GetDepartment(employee.DepartmentID));
                department.Employees = employeeRepository.GetEmployees()
                    .Where(e => e.DepartmentID == department.ID)
                    .ToList<EmployeeDTO>()
                    .Select(e => new Employee(e))
                    .ToList<Employee>();
            }
            catch (System.AggregateException ex)
            {
                // handle wrong httpClient endpoint Uri
                return new ValidationResult($"Wrong endpoint URI: {ex.Message}");
            }

            int? currentSalaries = 0;

            //Adding a new employee (check if the yearly budget is exceeded)
            if (employee.ID == 0)
            {
                currentSalaries = department.Employees.Sum(e => e.Salary);
                if ((currentSalaries * 12) + (Convert.ToInt32(value) * 12) > department.BudgetYearly)
                    return new ValidationResult($"'{employee.Salary}' salary would exceed yearly '{department.Name}' Department budget ('{department.BudgetYearly}')");
            
                return ValidationResult.Success;
            }

            //Editing an old employee (check if the yearly budget is exceeded)
            var empOldSalary = employeeRepository.GetEmployee(employee.ID).Salary;
            var oldDeptID = employeeRepository.GetEmployee(employee.ID).DepartmentID;

            if (oldDeptID == employee.DepartmentID)
                currentSalaries = department.Employees.Sum(e => e.Salary) - empOldSalary;
            else
                currentSalaries = department.Employees.Sum(e => e.Salary);

            if ((currentSalaries * 12) + (Convert.ToInt32(value)* 12) > department.BudgetYearly)
                return new ValidationResult($"'{employee.Salary}' salary would exceed yearly '{department.Name}' Department budget ('{department.BudgetYearly}')");

            return ValidationResult.Success;
        }
    }
}