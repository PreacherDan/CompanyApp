using Microsoft.AspNetCore.Mvc;
using CompanyApp.Models;
using CompanyApp.DTOs;
using CompanyApp.ViewModels;
using CompanyApp.Data;

namespace CompanyApp.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("employees/all")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("employees/details/{id}")]
        public IActionResult Details(int id)
        {
            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return BadRequest();

            return View(empFromDb);
        }

        public IActionResult New()
        {
            var viewModel = new EmployeeFormViewModel()
            {
                Employee = new DTOs.EmployeeDTO(),
                Departments = _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)).ToList<DepartmentDTO>()
            };

            return View("CustomerForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if(empFromDb == null) return BadRequest();

            var viewModel = new EmployeeFormViewModel()
            {
                Employee = new EmployeeDTO(empFromDb),
                Departments = _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)).ToList<DepartmentDTO>()
            };

            return View("CustomerForm", viewModel);
        }

        public IActionResult Save(EmployeeDTO employee)
        {


            return RedirectToAction("Index", "Employees");
        }
    }
}
