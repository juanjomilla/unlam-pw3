using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    public class GuardarNecesidadViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "No debe tener más de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es requerida.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El teléfono de contacto es requerido.")]
        [StringLength(30, ErrorMessage = "No debe tener más de {1} caracteres.")]
        public string TelefonoContacto { get; set; }

        [Required(ErrorMessage = "El tipo de donación es requerido.")]
        public TipoDeDonacion TipoDonacion { get; set; }

        public decimal CantDinero { get; set; }

        public string CBUAlias { get; set; }

        public ICollection<NecesidadesDonacionesInsumos> Insumos { get; set; }

        public ICollection<NecesidadesReferencias> Referencias { get; set; }

        public bool ModificandoDatos { get; set; }

        public int IdNecesidad { get; set; }

        public enum TipoDeDonacion
        {
            Monetaria = 0,
            Insumos
        }
    }
}