using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Imagem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Arquivo obrigatório", AllowEmptyStrings = false)]
        public string Arquivo { get; set; }

        public string ContentType { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Data de Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Data de Avaliação")]
        public Nullable<DateTime> DataAvaliacao { get; set; }

        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Display(Name = "Avaliador")]
        public string Avaliador { get; set; }

        public Imagem()
        {
            this.Arquivo = string.Empty;
            this.ContentType = string.Empty;
            this.Situacao = string.Empty;
            this.Usuario = string.Empty;
            this.Avaliador = string.Empty;
        }
    }
}

