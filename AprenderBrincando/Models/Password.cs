using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Password
    {
        public string Token { get; set; }

        [Required(ErrorMessage = "Senha obrigatório", AllowEmptyStrings = false)]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 12 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Confirmação da Senha é obrigatório", AllowEmptyStrings = false)]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 12 caracteres.")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação não correspondem.")]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmarSenha { get; set; }

        public Password()
        {
            this.Token = string.Empty;
            this.Senha = string.Empty;
            this.ConfirmarSenha = string.Empty;
        }

    }
}
