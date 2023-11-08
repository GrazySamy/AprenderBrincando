using AprenderBrincando.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AprenderBrincando.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categoria()
        {
            return View();
        } 
        public IActionResult SobreNos()
        {
            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Infantil()
        {
            return View();
        }

        public IActionResult Fundamental()
        {
            return View();
        }

        public IActionResult Mural()
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