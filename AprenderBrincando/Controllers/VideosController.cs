using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AprenderBrincando.Controllers
{
    public class VideosController : Controller
    {

        private readonly Repositories.ADO.SQLServer.Video repository;
        private readonly IConfiguration configuration;

        public VideosController(IConfiguration configuration) // objeto configuration => parte do framework que permite ler o arquivo appsettings.json - GetConnectionString => método do framework que permite ler a chave ConnectionStrings deste arquivo.
        {
            this.repository = new Repositories.ADO.SQLServer.Video(configuration.GetConnectionString(Configurations.Appsettings.getKeyConnectionString()));
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            return View(this.repository.getAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Video video)
        {
            try
            {
                this.repository.add(video);
                return RedirectToAction("Catalog");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(this.repository.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Models.Video video)
        {
            try
            {
                this.repository.update(id, video);
                return RedirectToAction("Catalog");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(this.repository.getById(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.repository.delete(id);
            return RedirectToAction("Catalog");
        }
    }
}
