using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace AprenderBrincando.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Repositories.ADO.SQLServer.Usuario repository;

        public UsuariosController(IConfiguration configuration) // objeto configuration => parte do framework que permite ler o arquivo appsettings.json - GetConnectionString => método do framework que permite ler a chave ConnectionStrings deste arquivo.
        {
            this.repository = new Repositories.ADO.SQLServer.Usuario(configuration.GetConnectionString(Configurations.Appsettings.getKeyConnectionString()));
            //Configurations.Appsettings.getKeyConnectionString => nossa classe de configuração para trazer a chave que deve ser lida, neste caso: DefaultConnection.
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Usuario usuario)
        {
            try
            {
                usuario.Senha = ComputeSha256Hash(usuario.Senha);
                this.repository.add(usuario);

                return RedirectToAction("Login", "Usuarios");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.Login login)
        {
            try
            {
                login.Senha = ComputeSha256Hash(login.Senha);

                Models.Usuario usuario = this.repository.getByLogin(login.Email);

                if (usuario == null)
                {
                    TempData["Mensagem"] = "Usuario não cadastrado";
                    return RedirectToAction("Login", "Usuarios");
                }
                else
                if(!login.Senha.Equals(usuario.Senha))
                {
                    TempData["Mensagem"] = "Senha ou Usuario inválido";
                    return RedirectToAction("Login", "Usuarios");
                }
                else
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, usuario.Email),
                        new Claim("Nome", usuario.Nome)   
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false, // Defina isso com base na escolha do usuário
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authProperties);


                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    
    }
}
