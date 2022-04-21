using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // eager loading - include()
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Data;
using CompanyApp.Models;
using CompanyApp.DTOs;

namespace CompanyApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private ApplicationDbContext _context = null;

        public DepartmentsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_context.Departments.Include(d => d.Employees).ToList<Department>().Select(d => new DepartmentDTO(d)));
        }
        
        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var deptFromDb = _context.Departments.Include(d =>d.Employees).SingleOrDefault(d => d.ID == id);
            if (deptFromDb == null) return NotFound();

            return Ok(new DepartmentDTO(deptFromDb));
        }
    }
}
