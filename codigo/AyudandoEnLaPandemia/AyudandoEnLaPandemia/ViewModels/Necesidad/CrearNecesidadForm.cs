using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AyudandoEnLaPandemia.CustomDataAnnotations;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    public class CrearNecesidadForm
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        public string TelefonoDeContacto { get; set; }

        [Required]
        public TipoDeDonacion TipoDonacion { get; set; }

        public int CantidadDinero { get; set; }

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