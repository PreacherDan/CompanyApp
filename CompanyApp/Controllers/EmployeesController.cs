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
            //_context.Database.EnsureCreated();
            //_context.Database.EnsureDeleted();
        }

        [Route("employees/all")]
        public IActionResult Index()
        {            
            return View(_context.Employees.ToList<Employee>().Select(e => new EmployeeDTO(e)).ToList<EmployeeDTO>());

            //_context.Database.EnsureDeleted();
            //return new ViewResult();
        }

        [Route("employees/details/{id}")]
        public IActionResult Details(int id)
        {
            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return BadRequest();

            return View(new EmployeeDTO(empFromDb));
        }        

        [HttpPost]
        public IActionResult Save(EmployeeDTO employee)
        {
            //employee.Department = _context.Departments.ToList<Department>().Single<Department>(d => d.ID == employee.ID);
            
            foreach(var dept in _context.Departments.ToList())
                if (dept.ID == employee.Department.ID) employee.Department = new DepartmentDTO(dept);

            if (!ModelState.IsValid)
            {
                var depts = _context.Departments.ToList();
                var viewModel = new EmployeeFormViewModel()
                {
                    Employee = employee,
                    Departments = _context.Departments.ToList().Select(d => new DepartmentDTO(d)).ToList()
                };

                Console.WriteLine($"Validation falied for {employee.Name} {employee.Surname}!");
                return View("EmployeeForm", viewModel);
            }
            
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Employee> entityEntry = null;
            if (employee.ID == 0) // creating new emp
                entityEntry = _context.Employees.Add(new Employee(employee));
            else // editing emp (todo-automapper)
            {
                var empFromDb = _context.Employees.Single(e => e.ID == employee.ID);
                empFromDb.Name = employee.Name;
                empFromDb.Salary = employee.Salary;
                empFromDb.Surname = employee.Surname;
                empFromDb.Department = new Department(employee.Department);
                empFromDb.IsOnLeave = employee.IsOnLeave;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Employees");
        }

        public IActionResult New()
        {
            var viewModel = new EmployeeFormViewModel()
            {
                Employee = new DTOs.EmployeeDTO(),
                Departments = _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)).ToList<DepartmentDTO>()
            };

            return View("EmployeeForm", viewModel);
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
    }
}