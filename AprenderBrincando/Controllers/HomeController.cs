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
        public IActionResult Mural()
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //public ActionResult Galeria()
        //{
        //    var images = new List<ImageViewModel>
        //    {
        //        new ImageViewModel { Id = 1, Description = "Imagem 1", Url = "~/Images/image1.jpg" },
        //        new ImageViewModel { Id = 2, Description = "Imagem 2", Url = "~/Images/image2.jpg" },
        //        new ImageViewModel { Id = 3, Description = "Imagem 3", Url = "~/Images/image3.jpg" }
        //    };

        //    ViewBag.Images = images;
        //    return View();
        //}

    }
}