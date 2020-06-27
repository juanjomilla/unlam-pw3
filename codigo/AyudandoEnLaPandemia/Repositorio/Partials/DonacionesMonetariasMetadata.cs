using System.ComponentModel.DataAnnotations;

namespace Repositorio
{
    internal class DonacionesMonetariasMetadata
    {
        public int IdNecesidadDonacionMonetaria { get; set; }
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Dinero { get; set; }

        [Required(ErrorMessage = "Adjuntar comprobante es obligatorio")]
        public string ArchivoTransferencia { get; set; }

    }
}