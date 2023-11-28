using System.ComponentModel.DataAnnotations;

namespace AprenderBrincando.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Link obrigatório", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 50 caracteres.")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 100 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        public string Subcategoria { get; set; }

        public Video()
        {
            this.Link = string.Empty;
            this.Descricao = string.Empty;
            this.Categoria = string.Empty;
            this.Subcategoria = string.Empty;
        }
    }
}

