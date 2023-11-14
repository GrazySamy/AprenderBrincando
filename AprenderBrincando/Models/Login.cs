using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatório", AllowEmptyStrings = false)]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Mínimo de 8 e máximo de 12 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Login()
        {
            this.Email = string.Empty;
            this.Senha = string.Empty;
        }
    }
}
