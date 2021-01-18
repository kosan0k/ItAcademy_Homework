using Lesson6_Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using It_AcademyHomework.Repository.Common;
using System;

namespace Lesson6_Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGenericRepository<Good> _goods;
        private readonly IGenericRepository<Catalog> _catalogs;

        private const string _usernameKey = "username";

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Good> goods, IGenericRepository<Catalog> catalogs)
        {
            _goods = goods ?? throw new ArgumentNullException(nameof(goods));
            _catalogs = catalogs ?? throw new ArgumentNullException(nameof(catalogs));
            _logger = logger;
        }

        public IActionResult Index()
        {
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
