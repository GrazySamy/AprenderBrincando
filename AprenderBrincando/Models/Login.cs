using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email obrigatório", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 30 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatório", AllowEmptyStrings = false)]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 12 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Login()
        {
            this.Email = string.Empty;
            this.Senha = string.Empty;
        }
    }
}
