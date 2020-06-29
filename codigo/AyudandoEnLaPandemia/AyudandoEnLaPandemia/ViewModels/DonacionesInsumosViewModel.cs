using Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class DonacionesInsumosViewModel : BaseViewModel
    {

        public int IdNecesidadDonacionInsumo { get; set; }
        public int IdNecesidad { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadRestante { get; set; }

       // [Required(ErrorMessage = "Ingresar Cantidad a donar")]
        public int CantidadAdonar { get; set; }
        public bool statusCompleto { get; set; }


    }
}