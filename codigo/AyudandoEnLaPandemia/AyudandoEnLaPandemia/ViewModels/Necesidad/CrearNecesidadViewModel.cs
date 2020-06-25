using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    public class CrearNecesidadViewModel : BaseViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        public string TelefonoContacto { get; set; }

        [Required]
        public TipoDeDonacion TipoDonacion { get; set; }

        public int CantDinero { get; set; }

        public string CBUAlias { get; set; }

        public ICollection<InsumoForm> Insumos { get; set; }

        public ICollection<ReferenciaForm> Referencias { get; set; }

        public enum TipoDeDonacion
        {
            Monetaria = 0,
            Insumos
        }
    }
}