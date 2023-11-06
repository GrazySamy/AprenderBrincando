using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Email
    {
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email obrigatório", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 30 caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "Email")]
        public string Para { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Email()
        {
            this.Nome = string.Empty;
            this.Para = string.Empty;
            this.Assunto = string.Empty;
            this.Conteudo = string.Empty;
        }
    }
}
