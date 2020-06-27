using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Repositorio
{
    internal class DonacionesMonetariasMetadata
    {
        public int IdNecesidadDonacionMonetaria { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Dinero { get; set; }

    }
}