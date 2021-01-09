using Lesson6_Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.AdoNet;

namespace Lesson6_Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string _usernameKey = "username";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var connectionString =
                @"Server = .\SQLExpress; Database = HomeworkDb; Trusted_Connection = True; MultipleActiveResultSets = true;";
            var lol = new SqlAdoNetGenericRepository<Catalog>(connectionString);

            var result = lol.AddAsync(new Catalog() {Name = "Nameasdasd"}).GetAwaiter().GetResult();

            return View();
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password) 
        {
            IActionResult result = null;
            if (!string.IsNullOrEmpty(username))
            {
                HttpContext.Session.SetString(_usernameKey, username);
                result = RedirectToAction(nameof(this.Index));
            }
            else 
            {
                result = View();
            }

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
