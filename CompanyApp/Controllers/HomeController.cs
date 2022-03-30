using CompanyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CompanyApp.Data;
using System.Data;
using System.Linq;

namespace CompanyApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;

            _context = context;//this._context = new ApplicationDbContext(options => option.UseSqlServer(options.ConnectionString));
            //_context.Database.EnsureCreated();
            //_context.Database.Migrate();
            //_context.Database.EnsureDeleted();
        }

        public IActionResult Index()
        {
            //var x = _context.Employees.ToList<Employee>();
            //_context.Employees.Inclu
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}