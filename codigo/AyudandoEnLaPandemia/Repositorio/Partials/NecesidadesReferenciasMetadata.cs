using System.ComponentModel.DataAnnotations;

namespace Repositorio
{
    internal class NecesidadesReferenciasMetadata
    {
        [StringLength(50, ErrorMessage = "No debe tener más de {1} caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "No debe tener más de {1} caracteres.")]
        public string Telefono { get; set; }
    }
}