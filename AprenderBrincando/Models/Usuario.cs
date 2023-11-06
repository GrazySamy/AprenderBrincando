using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 15 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 15 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Email obrigatório", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 30 caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Celular obrigatório", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 15 caracteres.")]
        public string Celular { get; set; }

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


        public Usuario()
        {
            this.Nome = string.Empty;
            this.Sobrenome = string.Empty;
            this.Email = string.Empty;
            this.Celular = string.Empty;
            this.Senha = string.Empty;
        }
    }
}

