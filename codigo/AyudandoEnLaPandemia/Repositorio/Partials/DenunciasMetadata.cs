using System.ComponentModel.DataAnnotations;

namespace Repositorio
{
    internal class DenunciasMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(300, ErrorMessage = "No debe tener más de {1} caracteres.")]
        public string Comentarios { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public virtual MotivoDenuncia MotivoDenuncia { get; set; }
    }
}