﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApp.Models;
using CompanyApp.DTOs;
using CompanyApp.ViewModels;
using CompanyApp.Data;
//using System.Data.SqlTypes;
//using System.Data.SqlClient;


namespace CompanyApp.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            this._context = context;
            //_context.Database.EnsureDeleted();  //delete migrations folder. run app, call ensuredeleted(). add-migration Initial, update-db, call ensurecreated()
            //_context.Database.EnsureCreated();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
            base.Dispose(disposing);
        }

        [Route("employees/all")]
        public IActionResult Index()
        {
            var empsWithDepts = _context.Employees.Include(e => e.Department).ToList<Employee>().Select(e => new EmployeeDTO(e)).ToList<EmployeeDTO>();
            return View(empsWithDepts);
            //return new ViewResult();
        }

        [Route("employees/details/{id}")]
        public IActionResult Details(int id)
        {
            var empFromDb = _context.Employees.Include(e => e.Department).SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return NotFound();

            return View(new EmployeeDTO(empFromDb));
        }        

        [HttpPost]
        public IActionResult Save([FromForm] EmployeeDTO employee)
        {
                    
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

            _context.Database.OpenConnection(); //not necessary
            try
            {
                if (employee.ID == 0) // creating new emp
                    _context.Employees.Add(new Employee(employee));
                else // editing emp (todo-automapper)
                {
                    var empFromDb = _context.Employees.Single(e => e.ID == employee.ID);

                    empFromDb.Name = employee.Name;
                    empFromDb.Salary = employee.Salary;
                    empFromDb.Surname = employee.Surname;
                    empFromDb.DepartmentID = employee.DepartmentID;
                    empFromDb.IsOnLeave = employee.IsOnLeave;
                }
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }

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
            if(empFromDb == null) return NotFound();

            var viewModel = new EmployeeFormViewModel()
            {
                Employee = new EmployeeDTO(empFromDb),
                Departments = _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)).ToList<DepartmentDTO>()
            };

            return View("EmployeeForm", viewModel);
        }

        public IActionResult Delete(int id, string actionToReturn, string controllerToReturn)
        {
            var empFromDb = _context.Employees.SingleOrDefault(e => e.ID == id);
            if (empFromDb == null) return NotFound();

            _context.Employees.Remove(empFromDb);
            _context.SaveChanges();

            return RedirectToAction(actionToReturn, controllerToReturn);
        }

        public IActionResult DepartmentDetails(int id)
        {
            var deptFromDb = _context.Departments.Include(d=>d.Employees).SingleOrDefault(d => d.ID == id);
            //var deptFromDb = _context.Departments.SingleOrDefault(d => d.ID == id);
            if (deptFromDb == null) return NotFound();

            var viewModel = new DepartmentDetailsViewModel()
            {
                Department = new DepartmentDTO(deptFromDb),
                Employees = _context.Employees.Where(e => e.DepartmentID == id).Select(e => new EmployeeDTO(e))
            };

            return View(viewModel);
        }

        public IActionResult IndexDepartments()
        {
            return View("Departmentindex", _context.Departments.ToList<Department>().Select(d => new DepartmentDTO(d)));
                //.ToList<DepartmentDTO>());
        }

        public IActionResult SaveDepartment(DepartmentDTO department)
        {
            if (!ModelState.IsValid)
                return View("DepartmentForm", department);

            if (department.ID == 0)
                _context.Departments.Add(new Department(department));
            else
            {
                var deptFromDb = _context.Departments.ToList<Department>().Single(d => d.ID == department.ID);

                deptFromDb.Name = department.Name;
                deptFromDb.BudgetYearly = department.BudgetYearly;
                deptFromDb.Location = department.Location;
            }

            _context.SaveChanges();
            return RedirectToAction("IndexDepartments");
        }

        public IActionResult CreateDepartment()
        {
            return View("DepartmentForm", new DepartmentDTO());
        }

        public IActionResult EditDepartment(int id)
        {
            var deptFromDb = _context.Departments.ToList<Department>().SingleOrDefault(d => d.ID == id);
            if (deptFromDb == null) return NotFound();

            return View("DepartmentForm", new DepartmentDTO(deptFromDb));
        }
    }
}