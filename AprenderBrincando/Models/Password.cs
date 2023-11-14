using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Password //Resetar senha
    {
        public string Token { get; set; }

        [StringLength(12, MinimumLength = 8, ErrorMessage = "Mínimo de 8 e máximo de 12 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Confirmação da Senha é obrigatório", AllowEmptyStrings = false)]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Mínimo de 8 e máximo de 12 caracteres.")]
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
