using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Mvc.Routing;
using Coravel.Mailer.Mail;



using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using static System.Net.WebRequestMethods;
using static Coravel.Mailer.Mail.Mailers.AssertMailer;
using Org.BouncyCastle.Asn1.Pkcs;
using AprenderBrincando.Models;


namespace AprenderBrincando.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IMailer mailer;
        private readonly Repositories.ADO.SQLServer.Usuario repository;
        private readonly IConfiguration configuration;

        public UsuariosController(IConfiguration configuration, IUrlHelperFactory urlHelperFactory, IMailer mailer) // objeto configuration => parte do framework que permite ler o arquivo appsettings.json - GetConnectionString => método do framework que permite ler a chave ConnectionStrings deste arquivo.
        {
            this.repository = new Repositories.ADO.SQLServer.Usuario(configuration.GetConnectionString(Configurations.Appsettings.getKeyConnectionString()));
            this.configuration = configuration;
            this.mailer = mailer;
            
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
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
            catch
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

                Models.Usuario usuario = this.repository.getByEmail(login.Email);

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
                        new Claim("Perfil", usuario.Perfil.ToString()),
                        new Claim("Nome", usuario.Nome),
                        new Claim("Id", usuario.Id.ToString())
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
            catch
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(Models.Email email)
        {
            try
            {
                Models.Usuario usuario = this.repository.getByEmail(email.Para);

                if (usuario == null)
                {
                    TempData["MsgForgotPassword"] = "Para recuperação da senha, acesse o link no e-mail " + email.Para;
                    return RedirectToAction("ForgotPassword", "Usuarios");
                }
                else
                {
                    Guid uuid = Guid.NewGuid();
                    this.repository.updateToken(usuario.Id, uuid.ToString());

                    byte[] tokenBytes = Encoding.ASCII.GetBytes(email.Para + "#" + uuid);
                    string token =  System.Convert.ToBase64String(tokenBytes);

                    var destinatarios = new List<MailRecipient> { new MailRecipient(usuario.Email) };

                    string nomeRemetente = configuration.GetValue<string>(Configurations.Appsettings.getKeyNomeRemetente());
                    string emailRemetente = configuration.GetValue<string>(Configurations.Appsettings.getKeyEmailRemetente());
                    var remetente = new MailRecipient(emailRemetente, nomeRemetente);

                    string assunto = "Recuperação de Senha";
                    string conteudo = "<p> Olá "+ usuario.Nome+ "! </p><p> Estamos felizes que você faz parte do site Aprender Brincando.Por favor, <a href='https://localhost:7147/Usuarios/ResetPassword?token=" + token + "'>click aqui</a> para confirmar a alteração de senha.</p>";

                    await this.mailer.SendAsync(conteudo, assunto , destinatarios, remetente, null, null, null);

                    TempData["MsgForgotPassword"] = "Para recuperação da senha, siga as orientações enviadas ao e-mail " + email.Para;
                    return RedirectToAction("ForgotPassword", "Usuarios");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(Models.Password password)
        {
            try
            {
                byte[] tokenBytes = System.Convert.FromBase64String(password.Token);
                string token = System.Text.ASCIIEncoding.ASCII.GetString(tokenBytes);
                string[] tokens = token.Split("#");

                Models.Usuario usuario = this.repository.getByEmailAndToken(tokens[0], tokens[1]);


                if (usuario == null)
                {
                    TempData["MsgForgotPassword"] = "Sua recuperação da senha expirou, solicite o reenvio.";
                    return RedirectToAction("ForgotPassword", "Usuarios");
                }
                else
                {
                    usuario.Senha = ComputeSha256Hash(password.Senha);
                    this.repository.update(usuario.Id, usuario);

                    return RedirectToAction("Login", "Usuarios");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

    }
}
