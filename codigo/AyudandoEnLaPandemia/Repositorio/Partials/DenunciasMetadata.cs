using System.ComponentModel.DataAnnotations;

namespace Repositorio
{
    internal class DenunciasMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Comentarios { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public virtual MotivoDenuncia MotivoDenuncia { get; set; }
    }
}