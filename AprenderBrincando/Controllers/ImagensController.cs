using AprenderBrincando.Models;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;

namespace AprenderBrincando.Controllers
{
    public class ImagensController : Controller
    {

        private readonly Repositories.ADO.SQLServer.Imagem repository;
        private readonly IConfiguration configuration;

        public ImagensController(IConfiguration configuration) // objeto configuration => parte do framework que permite ler o arquivo appsettings.json - GetConnectionString => método do framework que permite ler a chave ConnectionStrings deste arquivo.
        {
            this.repository = new Repositories.ADO.SQLServer.Imagem(configuration.GetConnectionString(Configurations.Appsettings.getKeyConnectionString()));
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            List <Imagem> imagens = this.repository.getAllByUser(Int32.Parse(User.FindFirstValue("Id")));
            return View(imagens);
        }

        [HttpGet]
        public IActionResult Approval()
        {
            List<Imagem> imagens = this.repository.getAll();
            return View(imagens);
        }

        public IActionResult Mural()
        {
            List<Imagem> imagens = this.repository.getAllByStatus('A');
            return View(imagens);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0)
            {
                try
                {
                    byte[] imageBytes;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileUpload.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    this.repository.add(imageBytes, fileUpload.ContentType, Int32.Parse(User.FindFirstValue("Id")));
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.repository.delete(id);
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Aprovar(int id)
        {
            this.repository.update(id, 'A', Int32.Parse(User.FindFirstValue("Id")));
            return RedirectToAction("Approval");
        }

        [HttpGet]
        public IActionResult Reprovar(int id)
        {
            this.repository.update(id, 'R', Int32.Parse(User.FindFirstValue("Id")));
            return RedirectToAction("Approval");
        }
    }
}
