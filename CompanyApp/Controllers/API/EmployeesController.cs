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
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(_context.Employees.Select(e => new EmployeeDTO(e)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var empFromDb = await _context.Employees.SingleOrDefaultAsync(e => e.ID == id);
            if (empFromDb == null) return NotFound($"Employee with {id} ID doesn't exist.");

            return  Ok(new EmployeeDTO(empFromDb));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            employee.ID = 0;
            await _context.Employees.AddAsync(new Employee(employee));

            await _context.SaveChangesAsync();
            return Ok(); //Created();
        }

        [HttpPut]
        [Route("api/employees/{id}")]
        public async Task<IActionResult> EditEmployee(int id, EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empFromDb = await _context.Employees.SingleOrDefaultAsync(e => e.ID == id);
            if (empFromDb == null) return NotFound($"No employee with {id} found!");

            empFromDb.Salary = employee.Salary;
            empFromDb.Surname = employee.Surname;
            empFromDb.Name = employee.Name;
            empFromDb.IsOnLeave = employee.IsOnLeave;
            empFromDb.DepartmentID = employee.DepartmentID;
            //empFromDb.Department = new Department(employee.Department); // should use ID navigation property, instead of an actual Dept instance

            await _context.SaveChangesAsync();
            return Ok(new EmployeeDTO(empFromDb));
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var empFromDb = await _context.Employees.SingleOrDefaultAsync(e => e.ID == id);
            if (empFromDb == null) return NotFound();

            _context.Employees.Remove(empFromDb);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}