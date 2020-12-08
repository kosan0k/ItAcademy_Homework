using Lesson4_Homework.Models;
using Lesson4_Homework.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lesson4_Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string _itAcademyWebsiteUri = "https://www.it-academy.by/";

        public HomeController(ILogger<HomeController> logger)
        {            
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ItAcademyRedirect() 
        {
            return Redirect(_itAcademyWebsiteUri);
        }

        public IActionResult CustomResult() 
        {
            return new CustomActionResult();
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
