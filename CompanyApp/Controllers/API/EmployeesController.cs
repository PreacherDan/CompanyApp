using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Data;
using CompanyApp.Models;
using CompanyApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase, IDisposable
    {

        // PMC> update-Package Microsoft.AspNet.WebApi –reinstall
        //install-Package Microsoft.AspNet.WebApi

        private ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            this._context = context;


        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        [HttpGet]
        [Route("/api/employees/")]
        public async Task<IActionResult> GetEmployees() // IHttpActionResult, ToListAsync
        {
            return Ok(_context.Employees.ToList<Employee>().Select(e => new EmployeeDTO(e))); 
        }

        [HttpGet("id")]
        //[Route("/api/employees/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var empFromDb = _context.Employees.ToList().SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return NotFound($"Employee with {id} ID doesn't exist.");

            return Ok(new EmployeeDTO(empFromDb));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            if (!ModelState.IsValid)            
                return BadRequest();

            _context.Database.OpenConnection();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Departments ON;");
                employee.ID = 0;
                _context.Employees.Add(new Employee(employee));

                //_context.Database.ExecuteSqlRaw();
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Departments OFF;");
            }
            finally
            {
                _context.Database.CloseConnection();
            }
            return Ok(); //Created();
        }

        //[HttpPut("id")]
        [HttpPut]
        [Route("api/employees/{id}")]
        //[Produces("application/json")]
        public async Task<IActionResult> EditEmployee(int id, EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return NotFound($"No employee with {id} found!");

            empFromDb.Salary = employee.Salary;
            empFromDb.Surname = employee.Surname;
            empFromDb.Name = employee.Name;
            empFromDb.IsOnLeave = employee.IsOnLeave;
            //empFromDb.Department = new Department(employee.Department); // should use ID navigation property, instead of an actual Dept instance
            empFromDb.DepartmentID = employee.DepartmentID; // should use ID navigation property, instead of an actual Dept instance

            _context.SaveChanges();
            return Ok(new EmployeeDTO(empFromDb));
        }

        [HttpDelete("id")]
        //[Produces("application/json")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return NotFound();

            _context.Employees.Remove(empFromDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
/*
                 var viewmodel = new CompanyApp.ViewModels.EmployeeFormViewModel
                {
                    Departments = _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)).ToList(),
                    Employee = new EmployeeDTO(new Employee(employee))
                };
            }*/